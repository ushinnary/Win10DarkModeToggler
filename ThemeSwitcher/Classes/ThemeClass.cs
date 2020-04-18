using Microsoft.Win32;
using System;
using ThemeSwitcher.Utils;
using Windows.Devices.Sensors;
using Windows.Foundation;

namespace ThemeSwitcher.Classes
{
    public class ThemeClass
    {

        public ThemeClass()
        {
            _lightSensor = LightSensor.GetDefault();
            _autoLightDetectionEnabled = false;

            SetWindowsVersion();
            SetWindowsRegistryKey();
        }

        private void SetWindowsVersion()
        {
            string[] versionsWithStartMenuTheme = { EnumList.WinVer1909 };
            _windowsVersion = RegistryClass.getCurrentWindowsVersion();
            _windowsVersionSupportStartMenu =
                Array.IndexOf(versionsWithStartMenuTheme, _windowsVersion) >= 0;
        }

        private void SetWindowsRegistryKey()
        {
            _windowsThemeRegistryKey = GetRegistryKeyForCurrentWindowsVersion();
        }

        private RegistryKey GetRegistryKeyForCurrentWindowsVersion()
        {
            RegistryKey key = null;

            if (
                _windowsVersion == EnumList.WinVer1909 ||
                _windowsVersion == EnumList.WinVer1903 ||
                _windowsVersion == EnumList.WinVer1809
            )
                key = RegistryClass.getThemeRegistryKey(true);

            return key;
        }

        public void ToggleAutoThemeSwitcherByLight()
        {
            _autoLightDetectionEnabled = !_autoLightDetectionEnabled;

            if (_autoLightDetectionEnabled)
                RunThemeByLightSwitcher();
        }

        public void RunThemeByLightSwitcher()
        {
            if (_lightSensor == null) return;

            _lightSensor.ReportInterval = 1000;
            _lightSensor.ReadingChanged += new TypedEventHandler<LightSensor, LightSensorReadingChangedEventArgs>(
                LightSensor_ReadingChanged);
        }

        private void LightSensor_ReadingChanged(LightSensor sender,
            LightSensorReadingChangedEventArgs args)
        {
            //LightSensorReading read = args.Reading;

            var currentLightSensorValue =
                Convert.ToUInt16(args.Reading.IlluminanceInLux);
            var currentThemeIsDark =
                Convert.ToUInt16(
                    _windowsThemeRegistryKey?.GetValue(EnumList
                        .ThemeKeyAppsThemeValue)) == 0;
            var toSetDarkTheme = currentLightSensorValue < 20;

            if (
                !currentThemeIsDark && toSetDarkTheme ||
                currentThemeIsDark && !toSetDarkTheme
                )
                SetTheme(toSetDarkTheme, true);
        }

        public void ToggleTheme()
        {
            SetTheme(false, false);
        }

        private void SetTheme(bool toSetDarkTheme, bool forceApply)
        {
            var key =
                GetRegistryKeyForCurrentWindowsVersion();

            if (key == null) return;

            if (!forceApply)
                toSetDarkTheme =
                    (int)key.GetValue(EnumList.ThemeKeyAppsThemeValue) == 1;

            key.SetValue(EnumList.ThemeKeyAppsThemeValue,
                toSetDarkTheme ? 0 : 1);

            if (_windowsVersionSupportStartMenu)
                key.SetValue(EnumList.ThemeKeySystemThemeValue,
                    toSetDarkTheme ? 0 : 1);

            key.Close();
        }

        #region PrivateProperties

        private readonly LightSensor _lightSensor;
        private bool _autoLightDetectionEnabled;
        private string _windowsVersion;
        private bool _windowsVersionSupportStartMenu;
        private RegistryKey _windowsThemeRegistryKey;

        #endregion
    }
}
