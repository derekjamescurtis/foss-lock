using System;
using System.Data;

namespace FossLock.Core
{
	public interface IEntityDbOperations
	{
		/// <summary>
		/// Gets the update command.
		/// </summary>
		/// <returns>
		/// The update command.
		/// </returns>
		/// <param name='cn'>
		/// Cn.
		/// </param>
		/// <param name='transaction'>
		/// Transaction.
		/// </param>
		IDbCommand GetUpdateCommand(IDbConnection cn = null, IDbTransaction transaction = null);

		/// <summary>
		/// Gets the delete command.
		/// </summary>
		/// <returns>
		/// The delete command.
		/// </returns>
		/// <param name='cn'>
		/// Cn.
		/// </param>
		/// <param name='transaction'>
		/// Transaction.
		/// </param>
		IDbCommand GetDeleteCommand(IDbConnection cn = null, IDbTransaction transaction = null);

		/// <summary>
		/// Gets the insert command.
		/// </summary>
		/// <returns>
		/// The insert command.
		/// </returns>
		/// <param name='cn'>
		/// Cn.
		/// </param>
		/// <param name='transaction'>
		/// Transaction.
		/// </param>
		IDbCommand GetInsertCommand(IDbConnection cn = null, IDbTransaction transaction = null);

	}
}

