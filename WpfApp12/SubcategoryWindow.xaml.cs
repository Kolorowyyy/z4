using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp12;

namespace WpfApp12
{
    public partial class SubcategoryWindow : Window
    {
        private XDocument xmlDoc;
        private string categoryName;
        private string subcategoryName;

        public SubcategoryWindow(XDocument xmlDoc, string categoryName, string subcategoryName)
        {
            InitializeComponent();
            this.xmlDoc = xmlDoc;
            this.categoryName = categoryName;
            this.subcategoryName = subcategoryName;
            LoadData();
        }

        private void LoadData()
        {
            var subcategoryElement = xmlDoc.Root.Elements("Category")
                .FirstOrDefault(c => c.Attribute("Name").Value == categoryName)
                ?.Elements("Subcategory")
                .FirstOrDefault(s => s.Attribute("Name").Value == subcategoryName);

            if (subcategoryElement != null)
            {
                var subcategoryDetails = subcategoryElement.Element("Details");
                if (subcategoryDetails != null)
                {
                    subcategoryDetailsTextBlock.Text = subcategoryDetails.Value;
                }

                var elements = subcategoryElement.Elements("Element");
                elementsDataGrid.ItemsSource = elements.Select(e => new
                {
                    Name = e.Attribute("Name").Value,
                    Year = e.Element("Year").Value,
                    EngineCapacity = e.Element("EngineCapacity").Value,
                    DriveType = e.Element("DriveType").Value
                });
            }
        }
    }
}
