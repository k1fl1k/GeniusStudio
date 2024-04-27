using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace GeniusStudio
{
    /// <summary>
    /// Логика взаимодействия для AddMenuPage.xaml
    /// </summary>
    public partial class AddMenuPage : Page
    {
        public AddMenuPage()
        {
            InitializeComponent();
        }

        string folders;
        string toDelete;

        private void BtnCreateCatalog_Click(object sender, RoutedEventArgs e)
        {
            SaveFileBtn.Visibility = Visibility.Visible;
            FileName.Visibility = Visibility.Visible;
            SaveTxtFileBtn.Visibility = Visibility.Collapsed;
            BtnCreateCataalog.Visibility = Visibility.Collapsed;
            BtnCreateSubCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteSubCatalog.Visibility = Visibility.Collapsed;
        }

        private void BtnCreateSubCatalog_Click(object sender, RoutedEventArgs e)
        {
            BtnCreateCataalog.Visibility = Visibility.Collapsed;
            BtnCreateSubCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteSubCatalog.Visibility = Visibility.Collapsed;
            string MenuFolderPath = "Menu";
            string[] menuFolders = Directory.GetDirectories(MenuFolderPath);

            foreach (string menuFolder in menuFolders)
            {
                string folderName = new DirectoryInfo(menuFolder).Name;
                folders = folderName;
                Button folderButton = new Button
                {
                    Content = folderName,
                    Width = 110,
                    Height = 100,
                    Margin = new Thickness(20),
                    Style = (Style)FindResource("ButtonStyle")
                };
                folderButton.Click += (s, args) =>
                {
                    SaveFileBtn.Visibility = Visibility.Collapsed;
                    FileName.Visibility = Visibility.Visible;
                    SaveTxtFileBtn.Visibility = Visibility.Visible;
                };
                SubMenuPage.Children.Add(folderButton);
            }
        }

        private void BtnDeleteCatalog_Click(object sender, RoutedEventArgs e)
        {
            BtnCreateCataalog.Visibility = Visibility.Collapsed;
            BtnCreateSubCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteSubCatalog.Visibility = Visibility.Collapsed;
            string MenuFolderPath = "Menu";
            string[] menuFolders = Directory.GetDirectories(MenuFolderPath);

            foreach (string menuFolder in menuFolders)
            {
                string folderName = new DirectoryInfo(menuFolder).Name;
                folders = folderName;
                Button folderButton = new Button
                {
                    Content = folderName,
                    Width = 400,
                    Height = 300,
                    FontSize = 24,
                    FontFamily = new FontFamily("Bahnschrift"),
                    Margin = new Thickness(20),
                    Style = (Style)FindResource("ButtonStyle")
                };
                folderButton.Click += (s, args) =>
                {
                    string toDelete = $"Menu/{folderName}";
                    try
                    {
                        Directory.Delete(toDelete, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося видалити папку '{folderName}': {ex.Message}");
                    }
                    NavigationService?.Navigate(new AddMenuPage());
                };
                SubMenuPage.Children.Add(folderButton);
            }
        }


        private void BtnDeleteSubCatalog_Click(object sender, RoutedEventArgs e)
        {
            BtnCreateCataalog.Visibility = Visibility.Collapsed;
            BtnCreateSubCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteCatalog.Visibility = Visibility.Collapsed;
            BtnDeleteSubCatalog.Visibility = Visibility.Collapsed;
            string MenuFolderPath = "Menu";
            string[] menuFolders = Directory.GetDirectories(MenuFolderPath);

            foreach (string menuFolder in menuFolders)
            {
                string folderName = new DirectoryInfo(menuFolder).Name;

                Button folderButton = new Button
                {
                    Content = folderName,
                    Width = 400,
                    Height = 300,
                    FontSize = 24,
                    FontFamily = new FontFamily("Bahnschrift"),
                    Margin = new Thickness(20),
                    Style = (Style)FindResource("ButtonStyle")
                };
                folderButton.Click += (sender, e) => LoadItemsFromFolder(folderName);

                SubMenuPage.Children.Add(folderButton);
            }
        }

        private void LoadItemsFromFolder(string folderName)
        {
            string folderPath = $"Menu/{folderName}";
            string[] itemFiles = Directory.GetFiles(folderPath);

            SubMenuPage.Children.Clear();

            foreach (string itemFile in itemFiles)
            {
                string itemName = System.IO.Path.GetFileNameWithoutExtension(itemFile);
                string[] itemInfo = File.ReadAllLines(itemFile);

                Button itemButton = new Button
                {
                    Content = itemName,
                    Width = 400,
                    Height = 300,
                    FontSize = 24,
                    FontFamily = new FontFamily("Bahnschrift"),
                    Margin = new Thickness(20),
                    Style = (Style)FindResource("ButtonStyle")
                };
                itemButton.Click += (sender, e) =>
                {
                    try
                    {
                        // Видаляємо файл, представлений поточним itemFile
                        File.Delete(itemFile);
                        MessageBox.Show($"Файл '{itemName}' був успішно видалений!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося видалити файл '{itemName}': {ex.Message}");
                    }

                    // Після видалення перенаправляємо на поточну сторінку знову
                    NavigationService?.Navigate(new AddMenuPage());
                };

                SubMenuPage.Children.Add(itemButton);
            }
        }



        private void SaveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string folderName = FileName.Text;

            // Перевірка, чи введена назва не порожня
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                try
                {
                    // Створення папки
                    Directory.CreateDirectory($"Menu/{folderName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося створити папку '{folderName}': {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Введіть назву папки!");
            }
        }

        private void SaveTxtFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileName.Text;

            // Перевірка, чи введена назва не порожня
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                try
                {
                    // Створення папки
                    File.Create($"Menu/{folders}/{fileName}.txt");
                    MessageBox.Show($"Папка '{fileName}' створена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося створити папку '{fileName}': {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Введіть назву папки!");
            }
        }
    }
}
