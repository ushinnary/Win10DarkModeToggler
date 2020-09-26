using System;
using Microsoft.Win32;
using ThemeByLight.Utils;

namespace ThemeByLight.Classes
{
	internal class RegistryClass
	{
		public static RegistryKey GetThemeRegistryKey(bool rw = false)
		{
			return Registry.CurrentUser.OpenSubKey(EnumList.ThemeKey, rw);
		}

		public static string GetCurrentWindowsVersion()
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(EnumList.WindowsVersionKey);

			if (key == null)
				return null;

			string version = Convert.ToString(key.GetValue(EnumList.WindowsVersionKeyValue));
			key.Close();
			return version;
		}
	}
}
