using System;
using System.Windows.Forms;
using ThemeByLight.Classes;

namespace ThemeByLight
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var theme = new ThemeClass();
            theme.ToggleAutoThemeSwitcherByLight();
            theme.RunThemeByLightSwitcher();

            using (NotifyIcon icon = new NotifyIcon())
            {
                icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                icon.ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Toggle light sensor", (s, e) => {theme.ToggleAutoThemeSwitcherByLight();}),
                    new MenuItem("Exit", (s, e) => { Application.Exit(); }),
                });
                icon.Visible = true;
                icon.Text = @"ThemeByLight";

                Application.Run();
                icon.Visible = false;
            }
        }

    }
}
