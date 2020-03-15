using System;
using Microsoft.Win32;
using WinDarkModeToggler.Utils;

namespace WinDarkModeToggler.Classes
{
	internal class RegistryClass
	{
		private RegistryClass()
		{
		}

		public static RegistryKey getThemeRegistryKey(bool rw = false)
		{
			return Registry.CurrentUser.OpenSubKey(EnumList.ThemeKey, rw);
		}

		public static String getCurrentWindowsVersion()
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(EnumList.WindowsVersionKey);

			if (key != null)
			{
				String version = Convert.ToString(key.GetValue(EnumList.WindowsVersionKeyValue));
				key.Close();
				return version;
			}

			return null;
		}
	}
}