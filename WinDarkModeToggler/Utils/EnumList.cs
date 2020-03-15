using System;

namespace WinDarkModeToggler.Utils
{
	internal class EnumList
	{
		private EnumList()
		{
		}

		public static readonly String WindowsVersionKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
		public static readonly String WindowsVersionKeyValue = "ReleaseId";

		public static readonly String ThemeKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
		public static readonly String ThemeKeyAppsThemeValue = "AppsUseLightTheme";
		public static readonly String ThemeKeySystemThemeValue = "SystemUsesLightTheme";

		public static readonly String WinVer1909 = "1909";
		public static readonly String WinVer1903 = "1903";
		public static readonly String WinVer1809 = "1809";
	}
}