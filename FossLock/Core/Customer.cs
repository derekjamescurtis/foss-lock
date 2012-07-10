using System;
using System.Data;
using System.Reflection;


namespace FossLock.Core
{
	/// <summary>
	/// Represents a Customer entity who may have 0..* Licenses associated with their account.
	/// </summary>
	public class Customer : Core.EntityBase
	{
		//TODO: when returning to work on this, the CRUD command methods need implemented.


		// logging for this class
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Initializes a new instance of the <see cref="FossLock.Core.Customer"/> class in the Added state.
		/// On AcceptChanges() this instance will be inserted into the database.
		/// </summary>
		internal Customer ()
		{ 
			// manually set the state to 'added' so we'll know to INSERT rather than UPDATE this object on AcceptChanges()
			this.ChangeState = EntityState.Added;

			// necessary for change rollbacks
			this._caretaker = new ShallowCaretaker<Customer>(this);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="FossLock.Core.Customer"/> class.
		/// The reader parameter must be positioned on the proper record before being passed to this constructor.
		/// </summary>
		/// <param name='reader'>
		/// Reader.
		/// </param>
		internal Customer (IDataReader reader)
		{
			// we'll need to rely on the reader being positioned properly before being passed to this constructor
			_id 				= (int)reader["Id"];
			_name 				= (string)reader["Name"];
			_address1 			= (string)reader["Address1"];
			_address2 			= (string)reader["Address2"];
			_city 				= (string)reader["City"];
			_state 				= (string)reader["State"];
			_postalCode 		= (string)reader["PostalCode"];
			_phone1 			= (string)reader["Phone1"];
			_phone2 			= (string)reader["Phone2"];
			_fax 				= (string)reader["Fax"];
			_email 				= (string)reader["Email"];
			_contactFirstName 	= (string)reader["ContactFirstName"];
			_contactLastName 	= (string)reader["ContactLastName"];

			// necessary for change rollbacks
			this._caretaker = new ShallowCaretaker<Customer>(this);
		}

		#region Properties

		/// <summary>
		///  Gets or sets a value that uniquely identifies this object within the database. When adding new objects, this
		/// property does not need to be manually set. 
		/// </summary>
		/// <remarks>
		///  Making a request to the property setter should cause an InvalidOperationException to be thrown in any derrived
		/// classes, if their current ChangeState is not set to 'Added' 
		/// </remarks>
		public override int Id 
		{
			get { return _id; }
			set 
			{
				if (value != Id)
				{
					if (ChangeState != System.Data.EntityState.Added)
						throw new NotImplementedException("Id property is currently automatically being set by the data provider and cannot be modified.");

					_id = value;
					OnPropertyChanged("Id");
				}
			}
		}
		int _id = 0;

		/// <summary>
		/// Gets or sets the Address1 property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Name 
		{
			get 
			{
				return _name;
			}
			set 
			{
				if (value != Name)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_name = value;
					OnPropertyChanged("CustomerName");
				}
			}
		}
		string _name = "New Customer";

		/// <summary>
		/// Gets or sets the Address1 property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Address1 
		{
			get  { return _address1; }
			set 
			{
				if (value != Address1)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_address1 = value;
					OnPropertyChanged("Address1");
				}
			}
		}
		string _address1 = string.Empty;

		/// <summary>
		/// Gets or sets the Address2 property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Address2 
		{
			get { return _address2; }
			set 
			{
				if (value != Address2)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_address2 = value;
					OnPropertyChanged("Address2");
				}
			}
		}
		string _address2 = string.Empty;

		/// <summary>
		/// Gets or sets the City property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string City {
			get { return _city; }
			set 
			{
				if (value != City)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_city = value;
					OnPropertyChanged("City");
				}
			}
		}
		string _city = string.Empty;

		/// <summary>
		/// Gets or sets the State abbreviation property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 2 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string State 
		{
			get 
			{
				return _state;
			}
			set 
			{
				if (value != State)
				{
					Core.Validation.ValidateString(value, 0, 2, false);
					_state = value;
					OnPropertyChanged("State");
				}
			}
		}
		string _state = string.Empty;

		/// <summary>
		/// Gets or sets the Postal Code property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 20 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string PostalCode 
		{
			get { return _postalCode; }
			set 
			{
				if (value != PostalCode)
				{
					Core.Validation.ValidateString(value, 0, 20, false);
					_postalCode = value;
					OnPropertyChanged("Zip");
				}
			}
		}
		string _postalCode = string.Empty;

		/// <summary>
		/// Gets or sets the primary phone number property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 25 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Phone1 
		{
			get { return _phone1; }
			set 
			{
				if (value != Phone1)
				{
					Core.Validation.ValidateString(value, 0, 25, false);
					_phone1 = value;
					OnPropertyChanged("Phone1");
				}
			}
		}
		string _phone1 = string.Empty;

		/// <summary>
		/// Gets or sets the secondary phone number property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 25 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Phone2 
		{
			get { return _phone2; }
			set 
			{
				if (value != Phone2)
				{
					Core.Validation.ValidateString(value, 0, 25, false);
					_phone2 = value;
					OnPropertyChanged("Phone2");
				}
			}
		}
		string _phone2 = string.Empty;

		/// <summary>
		/// Gets or sets the Fax number property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 25 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Fax 
		{
			get { return _fax; }
			set 
			{
				if (value != Fax)
				{
					Core.Validation.ValidateString(value, 0, 25, false);
					_fax = value;
					OnPropertyChanged("Fax");
				}
			}
		}
		string _fax = string.Empty;

		/// <summary>
		/// Gets or sets the E-mail address property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 90 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Email 
		{
			get { return _email; }
			set 
			{
				if (value != Email)
				{
					Core.Validation.ValidateString(value, 0, 90, false);
					_email = value;
					OnPropertyChanged("Email");
				}
			}
		}
		string _email = string.Empty;

		/// <summary>
		/// Gets or sets the Contact First Name property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string ContactFirstName 
		{
			get { return _contactFirstName; }
			set 
			{
				if (value != ContactFirstName)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_contactFirstName = value;
					OnPropertyChanged("ContactFirstName");
				}
			}
		}
		string _contactFirstName = string.Empty;

		/// <summary>
		/// Gets or sets the Contact First Name property.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string ContactLastName 
		{
			get { return _contactLastName; }
			set 
			{
				if (value != ContactLastName)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_contactLastName = value;
					OnPropertyChanged("ContactLastName");
				}
			}
		}
		string _contactLastName = string.Empty;

		#endregion		
		#region implemented abstract members of FossLock.Core.EntityBase

		// allows for rolling back changes made to this object.
		ShallowCaretaker<Customer> _caretaker;

		/// <summary>
		///  Commits any pending changes to the database, and resets this object's ChangeState to Unchanged. 
		/// </summary>
		public override void AcceptChanges()
		{		

			// get a connection object
			var cn = Actions.GetConnection();
			IDbCommand cmd;

			switch (this.ChangeState) 
			{
				case EntityState.Added:
					// we need to insert this record
					cmd = this.GetInsertCommand(cn, null);
					break;

				case EntityState.Deleted:
					// we need to delete this record
					cmd = this.GetDeleteCommand(cn, null);
					break;

				case EntityState.Modified:
					// update
					cmd = this.GetUpdateCommand(cn, null);
					break;

				case EntityState.Unchanged:
					// nothing to do!
					logger.Trace("No pending changes..");
					return;

				default:
					// unsupported
					throw new InvalidOperationException(string.Format ("Accepting changes for ChangeState: {0} is not supported.", this.ChangeState.ToString ()));
			}

			// try to execute the command
			int recordsAffected = cmd.ExecuteNonQuery();

			// make sure the expected number of records were affected
			if (recordsAffected != 1) throw new DBConcurrencyException();


			// update the state to reflect no pending changes
			this.ChangeState = EntityState.Unchanged;

			// create a new caretaker for the current state
			_caretaker = new ShallowCaretaker<Customer>(this);
		}

		/// <summary>
		///  Returns this object to the state it was in when it was created, or immediately after the last AcceptChanges()
		/// call was made. 
		/// </summary>
		public override void RejectChanges()
		{
			// rollback all the properties until we're in the proper state.
			_caretaker.RestoreState(this);

			// also, the ChangeState must be manually set.
			// when we're restoring properties, the change state will also be triggered to register Modified
			this.ChangeState  = _caretaker.SavedState.ChangeState;
		}

		#endregion		
		#region implemented abstract members of FossLock.Core.EntityBase

		/// <summary>
		///  Gets a provider-specific command to update this object in the database. 
		/// </summary>
		/// <returns>
		///  A command object assigned to the specified connection and enrolled in the specified transation. 
		/// </returns>
		/// <param name='cn'>
		///  The database connection that will generate the provider-specific command. This argument must be provided. 
		/// </param>
		/// <param name='transaction'>
		///  A reference to the provider-specific transation that this command should be enrolled in. This may be null. 
		/// </param>
		public override IDbCommand GetUpdateCommand (IDbConnection cn, IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		///  Gets a provider-specific command that will delete the underlying database record for this object. 
		/// </summary>
		/// <returns>
		///  A command object assigned to the specified connection and enrolled in the specified transation. 
		/// </returns>
		/// <param name='cn'>
		///  The database connection that will generate the provider-specific command. This argument must be provided. 
		/// </param>
		/// <param name='transaction'>
		///  A reference to the provider-specific transation that this command should be enrolled in. This may be null. 
		/// </param>
		public override IDbCommand GetDeleteCommand (IDbConnection cn, IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		///  Gets a provider-specific command that will insert this object into the database. 
		/// </summary>
		/// <returns>
		///  A command object assigned to the specified connection and enrolled in the specified transation. 
		/// </returns>
		/// <param name='cn'>
		///  The database connection that will generate the provider-specific command. This argument must be provided. 
		/// </param>
		/// <param name='transaction'>
		///  A reference to the provider-specific transation that this command should be enrolled in. This may be null. 
		/// </param>
		public override IDbCommand GetInsertCommand (IDbConnection cn, IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}
		#endregion

	}
}

