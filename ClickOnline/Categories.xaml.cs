using ClickOnline.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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

namespace ClickOnline
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Categories : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        public Categories()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CategoryAdd x = new CategoryAdd();
            x.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<CategoryViewModel> lCategory = new List<CategoryViewModel>();
            var categories = db.Categories.OrderBy(m => m.ProductCategory).ToList();
            int number = 1;
            foreach (var x in categories)
            {
                CategoryViewModel cat = new CategoryViewModel();
                cat.CategoryID = x.CategoryID;
                cat.Number = number;
                cat.ProductCategory = x.ProductCategory;
                number++;
                lCategory.Add(cat);
            }

            datagridview.ItemsSource = lCategory.ToList();
        }
    }
}
