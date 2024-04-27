using System;
using System.Windows;

namespace GeniusStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(string adminLevel)
        {
            InitializeComponent();
            if (adminLevel == "1")
            {
                BtnAddCatalog.Visibility = Visibility.Visible;
            }
            else if (adminLevel == "0")
            {
                BtnAddCatalog.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new BtnMenuPage();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ReLogin_Click(object sender, RoutedEventArgs e)
        {
            WinLog authorization = new WinLog();
            authorization.Show();
            this.Close();
        }

        private void BtnAddCatalog_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new AddMenuPage();
        }

        private void frame_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Content = new BtnMenuPage();
        }
    }
}
