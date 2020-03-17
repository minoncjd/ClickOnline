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
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        public ProductAdd()
        {
            InitializeComponent();
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (tbName.Text != "" && tbColor.Text != "" && cbCategory.Text != "" && cbSize.Text != "" && tbSellingPrize.Text != "" && tbPurchasePrize.Text != "")
                {
                    Product x = new Product();
                    x.ProductName = tbName.Text;
                    x.Color = tbColor.Text;
                    x.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                    x.SizeID = Convert.ToInt32(cbSize.SelectedValue);
                    x.SellingPrice = Convert.ToInt32(tbSellingPrize.Text);
                    x.PurchasePrice = Convert.ToInt32(tbPurchasePrize.Text);
                    db.Products.Add(x);
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    tbName.Text = "";
                    tbColor.Text = "";
                    cbCategory.Text = "";
                    cbSize.Text = "";
                    tbSellingPrize.Text = "";
                    tbPurchasePrize.Text = "";
                }
                else
                {
                    MessageBox.Show("complete the required fields");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbCategory.ItemsSource = db.Categories.OrderBy(m=>m.ProductCategory).ToList();
                cbCategory.DisplayMemberPath = "ProductCategory";
                cbCategory.SelectedValuePath = "CategoryID";

                cbSize.ItemsSource = db.Sizes.OrderBy(m => m.ProductSize).ToList();
                cbSize.DisplayMemberPath = "ProductSize";
                cbSize.SelectedValuePath = "SizeID";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            //Products x = new Products();
            //x.ShowDialog();
        }
    }
}
