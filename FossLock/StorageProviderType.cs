using System;

namespace FossLock
{
	/// <summary>
	/// Possible storage locations for the fosslock manager's running data.
	/// </summary>
	public enum StorageProviderType
	{
		/// <summary>
		/// SQLite provider.
		/// </summary>
		SqLite,
		/// <summary>
		/// Microsoft SQL Server provider
		/// </summary>
		MsSql,
		/// <summary>
		/// Oracle MySQL provider.
		/// </summary>
		MySql
	}
}

