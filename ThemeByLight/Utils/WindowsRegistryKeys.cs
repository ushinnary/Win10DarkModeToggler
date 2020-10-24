namespace ThemeByLight.Utils
{
	internal static class WindowsRegistryKeys
	{
		public static readonly string ThemeKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
		public static readonly string ThemeKeyAppsThemeValue = "AppsUseLightTheme";
		public static readonly string ThemeKeySystemThemeValue = "SystemUsesLightTheme";
		public static readonly string WindowsVersionKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
		public static readonly string WindowsVersionKeyValue = "ReleaseId";
	}
}