using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;


namespace FossLock
{
	/// <summary>
	/// Customer factory.
	/// </summary>
	public class CustomerFactory
	{

		// logging for this class
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


		private CustomerFactory () { }


		/// <summary>
		/// Gets the customers.
		/// </summary>
		/// <returns>
		/// The customers.
		/// </returns>
		public static IList<Core.Customer> GetCustomers()
		{

			var settings 	= Settings.GetInstance();
			var cn 			= Actions.GetConnection();
			var customers 	= new ObservableCollection<Core.Customer>();


			// make sure the Customers table has been initialized
			InitializeTable(settings.StorageType, cn);

			// query the table, get a list of customers.
			var cmdGetCustomers = cn.CreateCommand();
			cmdGetCustomers.CommandText = "SELECT * FROM Customers";

			var rdr = cmdGetCustomers.ExecuteReader();

			try 
			{				
				while (rdr.Read ()) 
					customers.Add (new FossLock.Core.Customer(rdr));

			} 
			catch(Exception ex)
			{
				logger.ErrorException("An error occurred while populating the Customers list.", ex);
				throw;
			}
			finally 
			{
				// make sure the reader has been closed and cleaned up after
				if (rdr != null && !rdr.IsClosed) 
					rdr.Close ();
			}


			return customers;

		}

		/// <summary>
		/// Creates the customer.
		/// </summary>
		/// <returns>
		/// The customer.
		/// </returns>
		/// <param name='list'>
		/// A list that the newly-created customer reference will be inserted into.  This parameter may be left null.
		/// </param>
		public static Core.Customer CreateCustomer(IList<Core.Customer> list = null)
		{
			throw new NotImplementedException();
		}


		// runs the creation script for the appropriate provider.
		static void InitializeTable(StorageProviderType type, IDbConnection cn)
		{
			var cmdInitializeType = cn.CreateCommand();

			// pull the appropriate command text from the dictionary
			cmdInitializeType.CommandText = _tableCreationScripts[type];

			// run the command
			cmdInitializeType.ExecuteNonQuery();

		}
	
		// dialect-specific database creation scripts for all the supported providers
		static Dictionary<StorageProviderType, string> _tableCreationScripts = new Dictionary<StorageProviderType, string>()
		{
			{ 
				StorageProviderType.MsSql, 
				@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
    				CREATE TABLE Customers (
					Id 				INT 			NOT NULL 	IDENTITY(1,1),
					Name 			NVARCHAR(50) 	NOT NULL,
					Address1 		NVARCHAR(50) 	NOT NULL,
					Address2 		NVARCHAR(50) 	NOT NULL,
					City 			NVARCHAR(50) 	NOT NULL,
					[State] 		NVARCHAR(2) 	NOT NULL,
					PostalCode 		NVARCHAR(20) 	NOT NULL,
					Phone1 			NVARCHAR(25) 	NOT NULL,
					Phone2 			NVARCHAR(25) 	NOT NULL,
					Fax 			NVARCHAR(25) 	NOT NULL,
					Email 			NVARCHAR(90) 	NOT NULL,
					ContactFirstName NVARCHAR(50) 	NOT NULL,
					ContactLastName NVARCHAR(50) 	NOT NULL,

					CONSTRAINT PK_CustomerId PRIMARY KEY CLUSTERED (Id ASC));"
			},
			{ 
				StorageProviderType.MySql, 
				@"CREATE TABLE IF NOT EXISTS `Customers` (
				  	`Id` 			INT 			NOT NULL 	AUTO_INCREMENT ,
				  	`Name` 			VARCHAR(50) 	NOT NULL ,
				  	`Address1` 		VARCHAR(50) 	NOT NULL ,
				  	`Address2` 		VARCHAR(50) 	NOT NULL ,
				  	`City` 			VARCHAR(50) 	NOT NULL ,
				  	`State` 		VARCHAR(2) 		NOT NULL ,
				  	`PostalCode` 	VARCHAR(20) 	NOT NULL ,
				  	`Phone1` 		VARCHAR(25) 	NOT NULL ,
				  	`Phone2` 		VARCHAR(25) 	NOT NULL ,
				  	`Fax` 			VARCHAR(25) 	NOT NULL ,
				  	`Email` 		VARCHAR(90) 	NOT NULL ,
				  	`ContactFirstName` VARCHAR(50) 	NOT NULL ,
				  	`ContactLastName` VARCHAR(50) 	NOT NULL ,
				  	PRIMARY KEY (`Id`) )
					ENGINE = InnoDB;" 
			},
			{ 	
				StorageProviderType.SqLite, 
				@"CREATE TABLE IF NOT EXISTS Customers (
					Id 				INTEGER NOT NULL 	PRIMARY KEY 	ASC,
					Name 			TEXT 	NOT NULL,
					Address1 		TEXT 	NOT NULL,
					Address2 		TEXT 	NOT NULL,
					City 			TEXT 	NOT NULL,
					State 			TEXT 	NOT NULL,
					PostalCode 		TEXT 	NOT NULL,
					Phone1 			TEXT 	NOT NULL,
					Phone2 			TEXT 	NOT NULL,
					Fax 			TEXT 	NOT NULL,
					Email 			TEXT 	NOT NULL,
					ContactFirstName TEXT 	NOT NULL,
					ContactLastName TEXT 	NOT NULL,
				);" 
			}
		};



	}
}

