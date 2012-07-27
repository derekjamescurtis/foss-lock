using System;

namespace FossLock.LicenseManager
{
	public partial class SettingsDialog : Gtk.Dialog
	{
		public SettingsDialog ()
		{
			this.Build ();

//
//			rdoMSSQL.Toggled += (object sender, EventArgs e) => 
//			{
//				var dlg = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Info, Gtk.ButtonsType.Ok, "Yo it worked!");
//				dlg.Run();
//				dlg.Destroy();
//			};

			
			this.entConnectionString.EditingDone += (sender, e) => 
			{ 
				var dlg = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Info, Gtk.ButtonsType.Ok, "Editing finished");
				dlg.Run();
				dlg.Destroy();
			};


		}

		public Gtk.RadioButton SQLiteRadioButton { get { return this.rdoSQLite; } }
		public Gtk.RadioButton MSSQLRadioButton { get { return this.rdoMSSQL; } }
		public Gtk.RadioButton MYSQLRadioButton { get { return this.rdoMYSQL; } }

		public Gtk.Entry ConnectionStringEntry { get { return this.entConnectionString; } }

		public Gtk.Button OkButton { get { return this.buttonOk; } }
		public Gtk.Button CancelButton { get { return this.buttonCancel; } }


	}
}

