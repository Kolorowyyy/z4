using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp12;

namespace WpfApp12
{
    public partial class MainWindow : Window
    {
        private XDocument xmlDoc;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                xmlDoc = XDocument.Load("data.xml");
                var categories = xmlDoc.Root.Elements("Category").Select(c => c.Attribute("Name").Value);
                categoryList.ItemsSource = categories;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Nie znaleziono pliku XML.");
            }
        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = categoryList.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var categoryWindow = new CategoryWindow(xmlDoc, selectedCategory);
                categoryWindow.ShowDialog();
            }
        }
    }
}