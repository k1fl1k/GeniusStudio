using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GeniusStudio
{
    /// <summary>
    /// Логика взаимодействия для BtnMenuPage.xaml
    /// </summary>
    public partial class BtnMenuPage : Page
    {
        public BtnMenuPage()
        {
            InitializeComponent();
            CreateMenuButtons();
        }

        private void CreateMenuButtons()
        {
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

                MenuPanel.Children.Add(folderButton);
            }
        }

        private void LoadItemsFromFolder(string folderName)
        {
            string folderPath = $"Menu/{folderName}";
            string[] itemFiles = Directory.GetFiles(folderPath);

            MenuPanel.Children.Clear();

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
                itemButton.Click += (sender, e) => DisplayItemInfo(itemFile);

                MenuPanel.Children.Add(itemButton);
            }
        }

            private void DisplayItemInfo(string itemFilePath)
            {
            MenuPanel.Children.Clear();

            string[] itemInfo = File.ReadAllLines(itemFilePath);

            try
            {
                foreach (string line in itemInfo)
                {
                    string[] itemData = line.Split('|');
                    if (itemData.Length >= 2)
                    {
                        TextBlock itemTextBlock = new TextBlock
                        {
                            FontFamily = new FontFamily("Bahnschrift"),
                            FontSize = 20,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF6E0")),
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Margin = new Thickness(20) // Add margin between TextBlocks
                        };

                        for (int i = 1; i < itemData.Length; i++)
                        {
                            string itemName = itemData[i].Trim(); // Extracting the item name
                            itemTextBlock.Text += itemName + Environment.NewLine;
                        }

                        MenuPanel.Children.Add(itemTextBlock);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

