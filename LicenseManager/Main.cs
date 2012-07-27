using System;
using Gtk;

namespace FossLock.LicenseManager
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//			Application.Init ();
			//			MainWindow win = new MainWindow ();
			//			win.Show ();
			//			Application.Run ();

			Application.Init ();

			SettingsDialog dlg = new SettingsDialog();





			dlg.Show();
			Application.Run ();
		}
	}
}
