using System.Windows.Forms;

namespace ThemeByLight.Classes
{
	public static class SysTrayIcon
	{
		public static void GenerateSystemTrayIcon(ThemeClass theme)
		{
			using (NotifyIcon icon = new NotifyIcon())
			{
				string togglerText = theme.IsSensorEnabled ? "Disable" : "Enable";
				MenuItem trigger = new MenuItem(togglerText, (s, e) => theme.ToggleAutoThemeSwitcherByLight())
				{
					Checked = theme.IsSensorEnabled
				};
				icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
				icon.ContextMenu = new ContextMenu(new MenuItem[] {
					trigger,
					new MenuItem("Exit", (s, e) => Application.Exit()),
				});
				icon.Visible = true;
				icon.Text = "ThemeByLight";

				Application.Run();
				icon.Visible = false;
			}
		}
	}
}
