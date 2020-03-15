using System;
using Microsoft.Win32;
using WinDarkModeToggler.Utils;

namespace WinDarkModeToggler.Classes
{
	internal class ThemeClass
	{
		private ThemeClass()
		{
		}

		public static bool isTimeToSetWhiteTheme()
		{
			int currentHour = DateTimeClass.getCurrentTime().Hour;

			return currentHour < 18 && currentHour >= 7;
		}

		public static void setTheme()
		{
			bool toSetDarkTheme = !isTimeToSetWhiteTheme();
			RegistryKey key;
			bool hasStartMenuTheme = false;

			String currentWinVersion = RegistryClass.getCurrentWindowsVersion();

			if (currentWinVersion == EnumList.WinVer1909)
			{
				key = RegistryClass.getThemeRegistryKey(true);
				hasStartMenuTheme = true;
			}
			else if (currentWinVersion == EnumList.WinVer1903 || currentWinVersion == EnumList.WinVer1809)
			{
				key = RegistryClass.getThemeRegistryKey(true);
			}
			else
			{
				key = null;
			}

			if (key != null)
			{
				key.SetValue(EnumList.ThemeKeyAppsThemeValue, toSetDarkTheme ? 0 : 1);

				if (hasStartMenuTheme)
				{
					key.SetValue(EnumList.ThemeKeySystemThemeValue, toSetDarkTheme ? 0 : 1);
				}

				key.Close();
			}
		}
	}
}