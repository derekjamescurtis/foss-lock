using System;
using System.Data.SQLite;
using System.Linq;

namespace FossLock.SqLite
{
	public class SqLiteCustomer  : Core.CustomerBase
	{ 


		internal SqLiteCustomer ()
		{
		}
		internal SqLiteCustomer (int customerId)
		{}
		internal SqLiteCustomer (SQLiteDataReader reader)
		{

		}


		public override void AcceptChanges ()
		{
			throw new System.NotImplementedException ();
		}




		public override void RejectChanges ()
		{
			throw new System.NotImplementedException ();
		}

	}
}

