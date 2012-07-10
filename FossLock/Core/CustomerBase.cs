using System;
using System.Reflection;

namespace FossLock.Core
{

	public class CustomerBase : Core.EntityBase, Core.ICustomer, Core.IEntityDbOperations
	{

		#region Customer implementation

		public override int Id 
		{
			get { return _id; }
			set 
			{
				if (value != Id)
				{
					if (ChangeState != System.Data.EntityState.Added)
						throw new NotImplementedException("Id property is currently automatically being set and cannot.");

					_id = value;
					OnPropertyChanged("Id");
				}
			}
		}
		int _id = 0;

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

		public string Zip 
		{
			get { return _zip; }
			set 
			{
				if (value != Zip)
				{
					Core.Validation.ValidateString(value, 0, 20, false);
					_zip = value;
					OnPropertyChanged("Zip");
				}
			}
		}
		string _zip = string.Empty;

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
		string _email;

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
		protected override System.Data.Common.DbCommand GetDeleteCommand (System.Data.IDbConnection cn)
		{
			throw new System.NotImplementedException ();
		}

		protected override System.Data.Common.DbCommand GetUpdateCommand (System.Data.IDbConnection cn)
		{
			throw new System.NotImplementedException ();
		}

		protected override System.Data.Common.DbCommand GetInsertCommand (System.Data.IDbConnection cn)
		{
			throw new System.NotImplementedException ();
		}

		public override void AcceptChanges ()
		{
			throw new System.NotImplementedException ();
		}

		public override void RejectChanges ()
		{
			throw new System.NotImplementedException ();
		}
		#endregion		

		#region IEntityDbOperations implementation
		public System.Data.IDbCommand GetUpdateCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		public System.Data.IDbCommand GetDeleteCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}

		public System.Data.IDbCommand GetInsertCommand (System.Data.IDbConnection cn, System.Data.IDbTransaction transaction)
		{
			throw new System.NotImplementedException ();
		}
		#endregion

		


	}
}

