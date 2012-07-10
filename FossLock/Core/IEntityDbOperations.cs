using System;
using System.Data;

namespace FossLock.Core
{
	/// <summary>
	/// Specifies the CRUD commands that can be generated for an individual object.
	/// </summary>
	public interface IEntityDbOperations
	{
		/// <summary>
		/// Gets a provider-specific command to update this object in the database.
		/// </summary>
		/// <returns>
		/// A command object assigned to the specified connection and enrolled in the specified transation.
		/// </returns>
		/// <param name='cn'>
		/// The database connection that will generate the provider-specific command.  This argument must be provided.
		/// </param>
		/// <param name='transaction'>
		/// A reference to the provider-specific transation that this command should be enrolled in.  This may be null.
		/// </param>
		IDbCommand GetUpdateCommand(IDbConnection cn, IDbTransaction transaction);

		/// <summary>
		/// Gets a provider-specific command that will delete the underlying database record for this object.
		/// </summary>
		/// <returns>
		/// A command object assigned to the specified connection and enrolled in the specified transation.
		/// </returns>
		/// <param name='cn'>
		/// The database connection that will generate the provider-specific command.  This argument must be provided.
		/// </param>
		/// <param name='transaction'>
		/// A reference to the provider-specific transation that this command should be enrolled in.  This may be null.
		/// </param>
		IDbCommand GetDeleteCommand(IDbConnection cn, IDbTransaction transaction);

		/// <summary>
		/// Gets a provider-specific command that will insert this object into the database.
		/// </summary>
		/// <returns>
		/// A command object assigned to the specified connection and enrolled in the specified transation.
		/// </returns>
		/// <param name='cn'>
		/// The database connection that will generate the provider-specific command.  This argument must be provided.
		/// </param>
		/// <param name='transaction'>
		/// A reference to the provider-specific transation that this command should be enrolled in.  This may be null.
		/// </param>
		IDbCommand GetInsertCommand(IDbConnection cn, IDbTransaction transaction);

	}
}

