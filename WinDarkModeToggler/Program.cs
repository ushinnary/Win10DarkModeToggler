using System;
using Microsoft.Win32;

namespace WinDarkModeToggler
{
	internal class Program
	{
		protected Program()
		{
		}

		private static void Main(string[] args)
		{
			setTheme(!isTimeToSetWhiteTheme());
		}

		private static DateTime getCurrentTime()
		{
			return DateTime.Now;
		}

		private static bool isTimeToSetWhiteTheme()
		{
			int currentHour = getCurrentTime().Hour;

			return currentHour <= 18 && currentHour >= 7;
		}

		private static String getCurrentWindowsVersion()
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

			if (key != null)
			{
				String version = Convert.ToString(key.GetValue("ReleaseId"));
				key.Close();
				return version;
			}

			return null;
		}

		private static void setTheme(bool toSetDarkTheme = false)
		{
			RegistryKey key;
			bool hasStartMenuTheme = false;
			switch (getCurrentWindowsVersion())
			{
				case "1909":
					key = getThemeRegistryKey(true);
					hasStartMenuTheme = true;
					break;

				case "1903":
				case "1809":
					key = getThemeRegistryKey(true);
					break;

				default:
					key = null;
					break;
			}

			if (key != null)
			{
				key.SetValue("AppsUseLightTheme", toSetDarkTheme ? 0 : 1);

				if (hasStartMenuTheme)
				{
					key.SetValue("SystemUsesLightTheme", toSetDarkTheme ? 0 : 1);
				}

				key.Close();
			}
		}

		private static RegistryKey getThemeRegistryKey(bool rw = false)
		{
			return Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", rw);
		}
	}
}