using NLog;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;

using System.IO;


namespace FossLock
{
	/// <summary>
	/// Provides access to application-wide user defined settings.
	/// </summary>
	public sealed class Settings : INotifyPropertyChanged, IRevertibleChangeTracking
	{
		#region Fields, Constructor, Singleton property

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // a connection that's held open to our sqlite database
        SQLiteConnection cn;
        Settings rollbackObject;

        Settings()
        {
            logger.Trace("Beginning init");


			// get a connection to the settings database
			cn = GetConnection ();


            // make sure our required table exists.             
			SQLiteCommand cmdCreateTable = this.GetCreateTableCommand(cn);
            cmdCreateTable.ExecuteNonQuery();            
                                   

            // get a reader for our local settings
            SQLiteCommand cmdGetLocalSettings = new SQLiteCommand(
                "SELECT StorageType, StorageLocation FROM LocalSettings WHERE Id = 1", cn);
            SQLiteDataReader rdr = cmdGetLocalSettings.ExecuteReader();

            // If our datarow exists -- we'll just pull data from it and dispose of the reader -- otherwise we're going to make a new datarow
            if (rdr.Read())
            {
                try
                {
                    // set the backing fields
                    _storageType 		= (StorageProviderType)(int)rdr[0];                   
					_storageLocation 	= new DbConnectionStringBuilder(){ ConnectionString = (string)rdr[1] };
                    
                    // close the reader
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    logger.ErrorException("error reading database", ex);
                }

            }
            else
            {
                // close the reader -- there's no data in the database, so we don't need it.
                rdr.Close();

                // insert a single record, with the default fields 
				var cmdInsert = GetInsertCommand(cn);
				cmdInsert.ExecuteNonQuery();  
            }
            
        }	

        /// <summary>
        /// Gets the instance of this class.
        /// Note: Each time this method is called, the same instance is returned.  
		/// The instance is initialized on the first request through this method.
        /// </summary>
        public static Settings GetInstance()
        {
            // make sure our singleton is initialized
            if (_singleton == null)
            {
                _singleton = new Settings();
                _singleton.rollbackObject = new Settings();
            }

            return _singleton;
        }
        static Settings _singleton;

		#endregion
        #region Instance Properties

		/// <summary>
		/// Gets or sets the type of the storage used for the application running data.
		/// See <see cref="StorageProviderType" /> for additional details.
		/// </summary>
		/// <remarks>Changing this property will cause StorageLocation to reset to a default value.</remarks>
        public StorageProviderType StorageType
        {
            get { return _storageType; }
            set
            {
                if (value != StorageType)
                {   
                    _storageType = value;

					// update the DbConnectionStringBuilder to a new instance -- clear any provider-specific connection string
					StorageLocation = new DbConnectionStringBuilder();

                    OnPropertyChanged("StorageType");
                }
            }
        }
		StorageProviderType _storageType = StorageProviderType.SqLite;

		/// <summary>
		/// Gets or sets a DbConnectionStringBuilder.  This may be set to a provider-specific instance
		/// object, but you should never assume that you're going to receive a provider-specific instance
		/// back from the GET accessor.
		/// </summary>
		public DbConnectionStringBuilder StorageLocation 
		{
			get { return _storageLocation; }			
			set
			{
				if (value != StorageLocation)
				{
					_storageLocation = value;
					OnPropertyChanged("StorageLocation");
				}
			}
		}
		DbConnectionStringBuilder _storageLocation = new DbConnectionStringBuilder();

        #endregion
        #region INotifyPropertyChanged Members

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name='propertyName'>
		/// Property name.
		/// </param>
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            this.IsChanged = true;
        }

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion        
        #region IRevertibleChangeTracking Members

        /// <summary>
        /// Resets the object’s state to unchanged by rejecting the modifications.
        /// </summary>
        public void RejectChanges()
        {
            // set all the properties to the values from the rollbackObject
            foreach (var propInfo in this.GetType().GetProperties())            
                propInfo.SetValue(this, propInfo.GetValue(rollbackObject, null), null);
            
            // set IsChanged back to false
            this.IsChanged = false;

			// grab a new instance of our rollback object
			rollbackObject = new Settings();
        }

        #endregion
        #region IChangeTracking Members

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
			if (this.IsChanged)
			{
				logger.Trace("AcceptChanges() called.");

				var cmdUpdate = this.GetUpdateCommand(cn);
				int rowsUpdated = cmdUpdate.ExecuteNonQuery();
				if (rowsUpdated != 1)
				{
					logger.Trace("Rows Updated: {0}, Expected {1}.  Throwing a DBConcurrencyException", rowsUpdated, 1);
					throw new DBConcurrencyException("AcceptChanges() did not update the expected number of records.");
				}

				logger.Trace("Changes successfully written to database.");

				// need to reset the IsChanged property to false
				this.IsChanged = false;

				// grab a new instance of our rollback object
				rollbackObject = new Settings();
			}
			else
			{
				logger.Trace("AcceptChanges() called, but no pending changes.  Skipping update command.");
			}
        }

        /// <summary>
        /// Gets the object's changed status.
        /// </summary>
        /// <returns>true if the object’s content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges"/>; otherwise, false.</returns>
        public bool IsChanged { get; private set; }

        #endregion
		#region Database Methods

		SQLiteConnection GetConnection()
		{
			// create a path to where the Settings.s3db file should be located
            var appDir = Actions.GetAppDataDirectory();

            string dbFilePath = appDir.FullName;
            if (!dbFilePath.EndsWith(Path.DirectorySeparatorChar.ToString())) // I'm not going to make any kind of assumption about whether the Directory string terminates with a separator character (it doesn't under windows.. maybe different under mono?)
                dbFilePath = string.Format("{0}{1}{2}", dbFilePath, Path.DirectorySeparatorChar, "Settings.s3db");
            else
                dbFilePath = string.Format("{0}{1}", dbFilePath, "Settings.s3db");


            // Create the database file if it doesn't already exist
            if (appDir.GetFiles("Settings.s3db").FirstOrDefault() == null)
            {                
                logger.Trace("Settings.s3db doesn't exist.  Creating new database file.");                
                logger.Trace("About to create database file at: {0}", dbFilePath);                
                SQLiteConnection.CreateFile(dbFilePath);
            }            
                        

            // Instantiate the Connection object
            SQLiteConnectionStringBuilder bldr = new SQLiteConnectionStringBuilder();
            bldr.DataSource = dbFilePath;
            cn = new SQLiteConnection(bldr.ConnectionString);


            // open the connection
            logger.Trace("Opening SQLite connection");
            cn.Open();

			// return the opened connection object
			return cn;
		}
		SQLiteCommand GetCreateTableCommand(SQLiteConnection cn)
		{
			var cmdCreateTable = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS LocalSettings 
					(
					Id INT PRIMARY KEY NOT NULL, 
					StorageType INT NOT NULL, 
					StorageLocation TEXT NOT NULL
					)", cn);

			return cmdCreateTable;
		}
		SQLiteCommand GetInsertCommand(SQLiteConnection cn)
		{
			var cmdInsert = new SQLiteCommand(
				@"INSERT INTO LocalSettings
					(Id, StorageType, StorageLocation)
					VALUES
					(@id, @storageType, @storageLocation
					)", cn);
				
			cmdInsert.Parameters.AddWithValue("@id", 1);
			cmdInsert.Parameters.AddWithValue("@storageType", (int)this.StorageType);
			cmdInsert.Parameters.AddWithValue("@storageLocation", this.StorageLocation);

			return cmdInsert;
		}
		SQLiteCommand GetUpdateCommand(SQLiteConnection cn)
		{
			var cmdUpdate = new SQLiteCommand(
					@"UPDATE LocalSettings
						SET StorageType = @storageType, StorageLocation = @storageLocation
						WHERE Id = @id", cn);

			cmdUpdate.Parameters.AddWithValue("@id", 1);
			cmdUpdate.Parameters.AddWithValue("@storageType", (int)this.StorageType);
			cmdUpdate.Parameters.AddWithValue("@storageLocation", this.StorageLocation);

			return cmdUpdate;

		}

		#endregion
	}
}

