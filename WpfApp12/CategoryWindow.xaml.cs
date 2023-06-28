using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp12;

namespace WpfApp12
{
    public partial class CategoryWindow : Window
    {
        private XDocument xmlDoc;
        private string categoryName;

        public CategoryWindow(XDocument xmlDoc, string categoryName)
        {
            InitializeComponent();
            this.xmlDoc = xmlDoc;
            this.categoryName = categoryName;
            LoadData();
        }

        private void LoadData()
        {
            var categoryElement = xmlDoc.Root.Elements("Category").FirstOrDefault(c => c.Attribute("Name").Value == categoryName);
            if (categoryElement != null)
            {
                var subcategories = categoryElement.Elements("Subcategory").Select(s => s.Attribute("Name").Value);
                subcategoryList.ItemsSource = subcategories;

                var categoryDetails = categoryElement.Element("Details");
                if (categoryDetails != null)
                {
                    categoryDetailsTextBlock.Text = categoryDetails.Value;
                }
            }
        }

        private void subcategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSubcategory = subcategoryList.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedSubcategory))
            {
                var subcategoryWindow = new SubcategoryWindow(xmlDoc, categoryName, selectedSubcategory);
                subcategoryWindow.ShowDialog();
            }
        }
    }
}
