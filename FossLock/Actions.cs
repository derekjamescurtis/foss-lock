using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;


namespace FossLock
{
	/// <summary>
	/// A static class that contains application-wide methods.
	/// </summary>
	internal static class Actions
	{
		// logging for this class
        private static NLog.Logger logger = NLog.LogManager.GetLogger("FossLock.Actions");

		/// <summary>
		/// Returns a DirectoryInfo object for a directory that can be expressed on a Windows OS as %appdata%\FossLock.
		/// If this directory does not exist, it is automatically created by this method.
		/// </summary>
        public static DirectoryInfo GetAppDataDirectory()
        {
            // get %appdata% 
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            logger.Trace("Resolved Application Data directory to: {0}", appDataPath);
            DirectoryInfo appDataDirInfo = new DirectoryInfo(appDataPath);


            // try to get info for %appdata%\FossLock
            var fosslockAppDataDirInfo = appDataDirInfo.GetDirectories("FossLock").FirstOrDefault();


            // %appdata%\FossLock doesn't exist, create it
            if (fosslockAppDataDirInfo == null)
            {
                logger.Trace("Fosslock subdirectory doesn't exist!  Creating..");
                fosslockAppDataDirInfo = appDataDirInfo.CreateSubdirectory("FossLock");
            }
            
            logger.Trace("FossLock appdata directory resolved to: {0}", fosslockAppDataDirInfo.FullName);

            return fosslockAppDataDirInfo;

        }

		/// <summary>
		/// Returns an opened, provider-specific connection string based on valued provided by the <see cref="Settings" /> class.
		/// </summary>
		public static IDbConnection GetConnection()
		{

			var settings = Settings.GetInstance();

			// this will be instantiated as a provider-specific connection object, but will have to be cast to the proper type 
			// by any client code.
			IDbConnection cn;

			logger.Trace ("About to create a database connection for type: {0}", settings.StorageType.ToString());

			switch (settings.StorageType) 
			{
				case StorageProviderType.SqLite:
					cn = new SQLiteConnection(settings.StorageLocation.ConnectionString);
					break;

				case StorageProviderType.MySql:
					cn = new MySqlConnection(settings.StorageLocation.ConnectionString);
					break;

				case StorageProviderType.MsSql:
					cn = new SqlConnection(settings.StorageLocation.ConnectionString);
					break;

				default:
					throw new NotImplementedException("The appropriate logic has not been implemented for the requested storage type provider.");

			}

			// Open the connection and return
			logger.Trace ("Connection created successfully from Settings.  About to open connection..");

			cn.Open ();

			logger.Trace("Connection opened successfully.");

			return cn;
		}

	}
}

