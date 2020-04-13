using System.Windows;
using ThemeSwitcher.Classes;

namespace ThemeSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public ThemeClass theme;

        public MainWindow()
        {
            InitializeComponent();
            theme = new ThemeClass();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            theme.ToggleTheme();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            theme?.ToggleAutoThemeSwitcherByLight();
        }
    }
}
