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
            try
            {
                using (var db = new ClickOnlineEntities())
                {
                    Category x = new Category();
                    x.ProductCategory = tbCategory.Text;
                    db.Categories.Add(x);
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    GetCategeries();
                    tbCategory.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetCategeries();
            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = true;
            btnClear.IsEnabled = true;
        }

        private void GetCategeries()
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
        private void Datagridview_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var x = ((CategoryViewModel)datagridview.SelectedItem);
            if (x!=null)
            {
                btnAdd.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnClear.IsEnabled = true;
                tbCategory.Text = x.ProductCategory;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var x = ((CategoryViewModel)datagridview.SelectedItem);
            var cat = db.Categories.Where(m => m.CategoryID == x.CategoryID).FirstOrDefault();
            cat.ProductCategory = tbCategory.Text;
            db.SaveChanges();
            MessageBox.Show("success");
            GetCategeries();
            tbCategory.Text = "";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbCategory.Text = "";
            btnClear.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnAdd.IsEnabled = true;
            tbCategory.Focus();

        }
    }
}
