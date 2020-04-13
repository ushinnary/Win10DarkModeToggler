using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using ThemeSwitcher.Utils;
using Windows.Devices.Sensors;


namespace ThemeSwitcher.Classes
{
    public class ThemeClass
    {
        public LightSensor lightSensor;
        public bool autoLightDetectionEnabled = false;
        public ThemeClass()
        {
            lightSensor = LightSensor.GetDefault();
        }
        private (RegistryKey, bool) GetRegistryKeyForCurrentWindowsVersion()
        {
            RegistryKey key;
            var hasStartMenuTheme = false;
            var currentWinVersion = RegistryClass.getCurrentWindowsVersion();

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
                return (null, false);
            }

            return (key, hasStartMenuTheme);
        }

        public void ToggleAutoThemeSwitcherByLight()
        {
            autoLightDetectionEnabled = !autoLightDetectionEnabled;

            if (autoLightDetectionEnabled)
                RunThemeByLightSwitcher();
        }

        public async void RunThemeByLightSwitcher()
        {
            if (lightSensor == null) return;

            while (autoLightDetectionEnabled)
            {
                var currentLightSensorValue =
                    Convert.ToUInt16(lightSensor?.GetCurrentReading()
                        ?.IlluminanceInLux);
                var (registryKey, _) = GetRegistryKeyForCurrentWindowsVersion();
                var currentThemeIsDark = Convert.ToUInt16(registryKey?.GetValue(EnumList.ThemeKeyAppsThemeValue)) == 0;

                if (!currentThemeIsDark && currentLightSensorValue <= 30)
                    SetTheme(true, true);
                else if (currentThemeIsDark && currentLightSensorValue >= 34)
                    SetTheme(false, true);

                await Task.Delay(3000);
            }
        }

        public void ToggleTheme()
        {
            SetTheme(false, false);
        }

        private void SetTheme(bool toSetDarkTheme, bool forceApply)
        {
            var (key, hasStartMenuTheme) = GetRegistryKeyForCurrentWindowsVersion();

            if (key == null) return;

            if (!forceApply)
                toSetDarkTheme = (int)key.GetValue(EnumList.ThemeKeyAppsThemeValue) == 1;

            key.SetValue(EnumList.ThemeKeyAppsThemeValue, toSetDarkTheme ? 0 : 1);

            if (hasStartMenuTheme)
                key.SetValue(EnumList.ThemeKeySystemThemeValue, toSetDarkTheme ? 0 : 1);

            key.Close();
        }
    }
}
