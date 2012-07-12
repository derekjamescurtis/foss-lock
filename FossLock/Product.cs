using System;
using System.Data;
using System.Data.Common;


namespace FossLock
{
	/// <summary>
	/// Product.
	/// </summary>
	public sealed class Product : Core.EntityBase
	{
		#region Constructor + Fields

		// logging for this class
		static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		// allows for rolling back changes made to this object.
		Core.ShallowCaretaker<Product> _caretaker;

		/// <summary>
		/// Initializes a new instance of the <see cref="FossLock.Product"/> class.
		/// </summary>
		internal Product ()
		{
			// manually set the state to 'added' so we'll know to INSERT rather than UPDATE this object on AcceptChanges()
			this.ChangeState = EntityState.Added;

			// necessary for change rollbacks
			this._caretaker = new Core.ShallowCaretaker<Product>(this);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="FossLock.Product"/> class.
		/// </summary>
		/// <param name='reader'>
		/// Reader.
		/// </param>
		internal Product(IDataReader reader)
		{
			_id 			= (int)reader["Id"];
			_name 			= (string)reader["Name"];
			_version 		= (string)reader["Version"];
			_lockProperties = (LockProperties)reader["LockProperties"];

			// necessary for change rollbacks
			this._caretaker = new Core.ShallowCaretaker<Product>(this);
		}

		#endregion
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
						throw new InvalidOperationException("Id property is currently automatically being set by the data provider and cannot be modified.");

					_id = value;
					OnPropertyChanged("Id");
				}
			}
		}
		int _id = 0;

		/// <summary>
		/// Gets or sets a short amount of text that identifies this instance to users.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 50 characters and may not be null, however an empty string is valid.
		/// </remarks>
		public string Name
		{
			get { return _name; }
			set
			{
				if (value != Name)
				{
					Core.Validation.ValidateString(value, 0, 50, false);
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}
		string _name = "New Product";

		/// <summary>
		/// Gets or sets a string that identifies the version.
		/// </summary>
		/// <remarks>
		/// This has a maximum of 10 characters and a minimum of 1 character.
		/// </remarks>
		public string Version
		{
			get { return _version; }
			set
			{
				if (value != Version)
				{
					Core.Validation.ValidateString(value, 1, 10, false);
					_version = value;
					OnPropertyChanged("Version");
				}
			}
		}
		string _version = "1.0";

		/// <summary>
		/// Gets or sets the system-identifier properties that the license is locked to.
		/// </summary>
		public LockProperties LockProperties
		{
			get { return _lockProperties; }
			set
			{
				if (value != LockProperties)
				{
					_lockProperties = value;
					OnPropertyChanged("LockProperties");
				}
			}
		}
		LockProperties _lockProperties = LockProperties.BIOS | LockProperties.CPU | LockProperties.Harddisk;


		#endregion
		#region Methods



		#endregion
		#region implemented abstract members of FossLock.Core.EntityBase

		public override void AcceptChanges ()
		{
			throw new System.NotImplementedException ();
		}

		public override void RejectChanges ()
		{
			throw new System.NotImplementedException ();
		}

		public override System.Data.IDbCommand GetUpdateCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		public override System.Data.IDbCommand GetDeleteCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		public override System.Data.IDbCommand GetInsertCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		#endregion

	}
}

