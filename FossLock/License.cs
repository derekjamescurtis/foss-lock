using System;
using System.Data;
using System.Data.Common;


namespace FossLock
{
	public sealed class License : Core.EntityBase
	{
		internal License()
		{
		}
		internal License(IDataReader reader)
		{
		}

		#region Properties

		public override int Id { get; set; }

		public Customer Customer { get; set; }

		public Product Product { get; set; }

		public DateTime GeneratedDate { get; set; }


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

