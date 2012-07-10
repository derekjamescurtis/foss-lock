using System;
using System.Data;
using System.ComponentModel;

namespace FossLock.Core
{
	/// <summary>
	/// The base class for all major business entities in this application.
	/// </summary>
	public abstract class EntityBase : INotifyPropertyChanged, IRevertibleChangeTracking, IEntityDbOperations
	{
		#region Properties
		
		/// <summary>
		/// Gets or sets a value that uniquely identifies this object within the database.
		/// When adding new objects, this property does not need to be manually set.  
		/// </summary>
		/// <remarks>
		/// Making a request to the property setter should cause an InvalidOperationException 
		/// to be thrown in any derrived classes, if their current ChangeState is not set to 'Added'
		/// </remarks>
		public abstract int Id { get; set; }

		/// <summary>
		/// Gets or sets the state of the change.
		/// </summary>
		/// <value>
		/// The state of the change.
		/// </value>
		public EntityState ChangeState
		{
			get { return _state; }
			protected internal set
			{
				if (value != ChangeState)
					_state = value;
			}
		}
		EntityState _state = EntityState.Unchanged;

		#endregion
		#region Methods

		/// <summary>
		/// Raises the property changed event.  Calling this method will also cause the ChangeState and IsChanged
		/// properties to update.  
		/// </summary>
		/// <param name='propertyName'>
		/// Property name.
		/// </param>
		protected void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));


			if (this.ChangeState != EntityState.Added || this.ChangeState != EntityState.Detached || this.ChangeState != EntityState.Deleted)
			{
				this.ChangeState = EntityState.Modified;
			}


		}
	

		#endregion
		#region Events

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;		

		#endregion
		#region IChangeTracking implementation

		/// <summary>
		/// Commits any pending changes to the database, and resets this object's ChangeState to Unchanged.
		/// </summary>
		public abstract void AcceptChanges ();

		/// <summary>
		/// Gets or sets a value indicating whether this instance is changed.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is changed; otherwise, <c>false</c>.
		/// </value>
		public bool IsChanged 
		{
			get  
			{
				return (this.ChangeState == EntityState.Unchanged) ? false : true;					
			}
		}

		#endregion		
		#region IRevertibleChangeTracking implementation

		/// <summary>
		/// Returns this object to the state it was in when it was created, or immediately after the last AcceptChanges() call was made.
		/// </summary>
		public abstract void RejectChanges ();


		#endregion		
		#region IEntityDbOperations implementation

		/// <summary>
		/// When overridden in a derrived class, returns a provider-specific command that will update this object's data in 
		/// the underlying database.
		/// </summary>
		/// <param name='cn'>
		///  Cn. 
		/// </param>
		/// <param name='transaction'>
		///  Transaction. 
		/// </param>
		public abstract IDbCommand GetUpdateCommand (IDbConnection cn, IDbTransaction transaction);
		/// <summary>
		/// When overridden in a derrived class, returns a provider-specific command that will remove this object's data from 
		/// the underlying database.
		/// </summary>
		/// <param name='cn'>
		///  Cn. 
		/// </param>
		/// <param name='transaction'>
		///  Transaction. 
		/// </param>
		public abstract IDbCommand GetDeleteCommand (IDbConnection cn, IDbTransaction transaction);
		/// <summary>
		/// When overridden in a derrived class, returns a provider-specific command that will insert this object's data in 
		/// the underlying database.
		/// </summary>
		/// <param name='cn'>
		///  Cn. 
		/// </param>
		/// <param name='transaction'>
		///  Transaction. 
		/// </param>
		public abstract IDbCommand GetInsertCommand (IDbConnection cn, IDbTransaction transaction);

		#endregion
	}
}

