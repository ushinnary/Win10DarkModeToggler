namespace ThemeByLight.Utils
{
    internal class EnumList
    {
        public static readonly string WindowsVersionKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        public static readonly string WindowsVersionKeyValue = "ReleaseId";

        public static readonly string ThemeKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        public static readonly string ThemeKeyAppsThemeValue = "AppsUseLightTheme";
        public static readonly string ThemeKeySystemThemeValue = "SystemUsesLightTheme";
                               
        public static readonly string WinVer1909 = "1909";
        public static readonly string WinVer1903 = "1903";
        public static readonly string WinVer1809 = "1809";
    }
}
