using System;
using System.Drawing;
using System.Windows.Forms;
using ThemeSwitcher.Classes;
using Application = System.Windows.Application;

namespace ThemeSwitcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon notifyIcon = new NotifyIcon();
        public App()
        {
            var theme = new ThemeClass();
            theme?.ToggleAutoThemeSwitcherByLight();
            theme?.RunThemeByLightSwitcher();

            // notifyIcon.Icon = new Icon(@"");
        }
    }
}
