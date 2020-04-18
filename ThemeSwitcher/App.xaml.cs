using System.Windows;
using ThemeSwitcher.Classes;

namespace ThemeSwitcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var theme = new ThemeClass();
            theme?.ToggleAutoThemeSwitcherByLight();
        }
    }
}
