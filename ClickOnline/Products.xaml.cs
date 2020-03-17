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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Products : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<ProductViewModel> lProduct = new List<ProductViewModel>();
        public Products()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            ProductAdd x = new ProductAdd();
            x.ShowDialog();
        }

        private void GetProducts()
        {
            try
            {
                int prodNumber = 1;
                lProduct = new List<ProductViewModel>();
                var products = db.GetProductList().OrderBy(m => m.ProductName).ToList();

                foreach (var x in products)
                {
                    ProductViewModel product = new ProductViewModel();
                    product.ProductID = x.ProductID;
                    product.Number = prodNumber;
                    product.ProductName = x.ProductName;
                    product.Color = x.Color;
                    product.Size = x.ProductSize;
                    product.Category = x.ProductCategory;
                    product.Color = x.Color;
                    product.PurchasePrice = x.PurchasePrice;
                    product.SellingPrice = x.SellingPrice;
                    prodNumber++;
                    lProduct.Add(product);
                }
                datagridview.ItemsSource = lProduct.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetProducts();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            var x = ((ProductViewModel)datagridview.SelectedItem);
            ProductUpdate xx = new ProductUpdate();
            xx.productid = x.ProductID;
            xx.ShowDialog();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var keyword = tbSearch.Text;
                datagridview.ItemsSource = lProduct.Where(m => m.ProductID.ToString().Contains(keyword) || m.ProductName.Contains(keyword) || m.Color.Contains(keyword) || m.Category.Contains(keyword) || m.SKUNo.Contains(keyword) || m.Size.Contains(keyword) ).ToList();
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text == "")
            {
                GetProducts();
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetProducts();
        }
    }
}
