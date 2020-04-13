using Microsoft.Win32;
using System;
using ThemeSwitcher.Utils;

namespace ThemeSwitcher.Classes
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

        public static string getCurrentWindowsVersion()
        {
            var key = Registry.LocalMachine.OpenSubKey(EnumList.WindowsVersionKey);

            if (key == null)
            {
                return null;
            }

            var version = Convert.ToString(key.GetValue(EnumList.WindowsVersionKeyValue));
            key.Close();
            return version;
        }
    }
}
