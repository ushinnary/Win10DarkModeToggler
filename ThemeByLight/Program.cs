using System;
using System.Windows.Forms;
using ThemeByLight.Classes;

namespace ThemeByLight
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			ThemeClass theme = new ThemeClass();
			theme.ToggleAutoThemeSwitcherByLight();
			theme.RunThemeByLightSwitcher();

			SysTrayIcon.GenerateSystemTrayIcon(theme);
		}
	}
}
