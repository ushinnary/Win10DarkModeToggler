using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using ThemeByLight.Utils;
using Windows.Devices.Sensors;

namespace ThemeByLight.Classes
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

		public void RunThemeByLightSwitcher()
		{
			if (_lightSensor == null)
				return;

			_lightSensor.ReportInterval = 500;
			_lightSensor.ReadingChanged += LightSensor_ReadingChanged;
		}

		public bool IsSensorEnabled => _autoLightDetectionEnabled;

		public void ToggleAutoThemeSwitcherByLight()
		{
			_autoLightDetectionEnabled = !_autoLightDetectionEnabled;
		}

		private RegistryKey GetRegistryKeyForCurrentWindowsVersion()
		{
			Regex regex = new Regex("(1809|^19|^20)");
			return regex.IsMatch(_windowsVersion) ? RegistryClass.GetThemeRegistryKey(true) : null;
		}

		private void LightSensor_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
		{
			if (!_autoLightDetectionEnabled)
				return;

			ushort currentLightSensorValue = Convert.ToUInt16(args.Reading.IlluminanceInLux);
			bool currentThemeIsDark = Convert.ToUInt16(_windowsThemeRegistryKey?.GetValue(WindowsRegistryKeys.ThemeKeyAppsThemeValue)) == 0;
			bool toSetDarkTheme = currentLightSensorValue < 20;
			bool hasToChangeTheme = currentThemeIsDark ^ toSetDarkTheme;

			if (hasToChangeTheme)
				SetTheme(toSetDarkTheme, true);
		}

		private void SetTheme(bool toSetDarkTheme, bool forceApply)
		{
			RegistryKey key = GetRegistryKeyForCurrentWindowsVersion();

			if (key == null)
				return;

			if (!forceApply)
				toSetDarkTheme = (int)key.GetValue(WindowsRegistryKeys.ThemeKeyAppsThemeValue) == 1;

			int themeId = toSetDarkTheme ? 0 : 1;

			key.SetValue(WindowsRegistryKeys.ThemeKeyAppsThemeValue, themeId);

			if (_windowsVersionSupportStartMenu)
				key.SetValue(WindowsRegistryKeys.ThemeKeySystemThemeValue, themeId);

			key.Close();
		}

		private void SetWindowsRegistryKey()
		{
			_windowsThemeRegistryKey = GetRegistryKeyForCurrentWindowsVersion();
		}

		private void SetWindowsVersion()
		{
			Regex regex = new Regex("(1909|^2)");
			_windowsVersion = RegistryClass.GetCurrentWindowsVersion();
			_windowsVersionSupportStartMenu = regex.IsMatch(_windowsVersion);
		}

		#region PrivateProperties

		private readonly LightSensor _lightSensor;
		private bool _autoLightDetectionEnabled;
		private RegistryKey _windowsThemeRegistryKey;
		private string _windowsVersion;
		private bool _windowsVersionSupportStartMenu;

		#endregion PrivateProperties
	}
}