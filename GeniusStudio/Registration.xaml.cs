using GeniusStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeniusStudio.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        static String Data = @"Data/";
        static String loginFile = "Login.txt";

        public Registration()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTxtBox.Text;
            string password = PasswordBox.Password;
            string admin;

            if (AdminPasswordBox.Password == "860790")
            {
                admin = "1";
            }
            else
            {
                admin = "0";
            }    
            string data = $"{login},{password},{admin}";

            using (StreamWriter writer = new StreamWriter(Data + loginFile, true))
            {
                writer.WriteLine(data);
                writer.Close();
            }
            MessageBox.Show ("Зареестровано");

        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminPasswordBox.Visibility = Visibility.Visible;
            AdminText.Visibility = Visibility.Visible;
            AdminBtn.Visibility = Visibility.Collapsed;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WinLog login = new WinLog();
            login.Show();
            this.Close();
        }
    }
}
