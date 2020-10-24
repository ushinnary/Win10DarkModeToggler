using System;
using Microsoft.Win32;
using ThemeByLight.Utils;

namespace ThemeByLight.Classes
{
	internal static class RegistryClass
	{
		public static string GetCurrentWindowsVersion()
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(WindowsRegistryKeys.WindowsVersionKey);

			if (key == null)
				return null;

			string version = Convert.ToString(key.GetValue(WindowsRegistryKeys.WindowsVersionKeyValue));
			key.Close();
			return version;
		}

		public static RegistryKey GetThemeRegistryKey(bool rw = false)
		{
			return Registry.CurrentUser.OpenSubKey(WindowsRegistryKeys.ThemeKey, rw);
		}
	}
}