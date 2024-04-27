using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Security.Cryptography;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GeniusStudio;
using GeniusStudio.Windows;

namespace GeniusStudio
{
    /// <summary>
    /// Логика взаимодействия для WinLog.xaml
    /// </summary>
    public partial class WinLog : Window
    {

        static String data = @"Data\";
        static String login = "Login.txt";

        public WinLog()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            String loginTxtBox = LoginTxt.Text;
            String passwordPassBox = Pass.Password;

            string filePath = Path.Combine(data, login);

            try
            {
                String[] lines = File.ReadAllLines(filePath);

                bool loginSuccessful = false;

                foreach (string line in lines)
                {
                    string[] elements = line.Split(',');

                    if (elements.Length >= 3 && elements[0] == loginTxtBox && elements[1] == passwordPassBox)
                    {
                        string adminLevel = elements[2];

                        if (adminLevel == "1")
                        {
                            MainWindow newWindow = new MainWindow(adminLevel);
                            newWindow.Show();
                            this.Close();
                        }
                        else if (adminLevel == "0")
                        {
                            MainWindow newWindow = new MainWindow(adminLevel);
                            newWindow.Show();
                            this.Close();
                        }

                        loginSuccessful = true;
                        break;
                    }
                }

                if (!loginSuccessful)
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error reading the file: " + ex.Message); }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
