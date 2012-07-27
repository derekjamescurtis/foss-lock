using System;
using System.ComponentModel;



namespace FossLock.LicenseManager
{
	public class SettingsDialogPresenter
	{

		readonly SettingsDialog _view;
		readonly Settings _model;

		public SettingsDialogPresenter (SettingsDialog view, Settings model)
		{
			// set a reference to the view and the model
			_view 	= view;
			_model 	= model;

			// populate initial values inside of our view
			InitializeView();


			// setup the view
			_model.PropertyChanged 	+= new PropertyChangedEventHandler(View_PropertyChangedHandler);


			// subscribe to the event handlers
			_view.OkButton.Clicked += View_OkButton_Click;
			_view.CancelButton.Clicked += View_CancelButton_Click;

		}

		void InitializeView()
		{
			switch(_model.StorageType)
			{
				case StorageProviderType.SqLite:
					_view.SQLiteRadioButton.Active = true;
					break;
				case StorageProviderType.MsSql:
					_view.MSSQLRadioButton.Active = true;
					break;
				case StorageProviderType.MySql:

					break;
				default:
					throw new NotImplementedException("The current provider type has not been implemented in this view.");

			}
		}

		void View_PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName) 
			{
				case "StorageType" : 

					// Manually check the appropriate radio button based on our type.

					if (_model.StorageType == StorageProviderType.SqLite) 
						_view.SQLiteRadioButton.Active = true;
					else if (_model.StorageType == StorageProviderType.MySql) 
						_view.MYSQLRadioButton.Active = true;
					else if (_model.StorageType == StorageProviderType.MsSql) 
						_view.MSSQLRadioButton.Active = true;
					else 
						throw new ArgumentException("Unable to determine the provider type.");

					break;


				case "StorageLocation" : 
				
					// Update the entry with the full connection string
					_view.ConnectionStringEntry.Text = _model.StorageLocation.ConnectionString;
					break;

				default: 
					break;
			}
		}

		void View_OkButton_Click(object sender, EventArgs e)
		{
		}
		void View_CancelButton_Click(object sender, EventArgs e)
		{

		}


	}
}

