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
    /// Interaction logic for ReportInventory.xaml
    /// </summary>
    public partial class ReportInventory : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<ProductViewModel> lProductViewModel = new List<ProductViewModel>();
        List<OrderViewModel> lOrderViewModel = new List<OrderViewModel>();

        public ReportInventory()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lProductViewModel = new List<ProductViewModel>();
            var products = db.GetProductList().ToList();
            foreach (var x in products)
            {
                ProductViewModel product = new ProductViewModel();
                product.ProductID = x.ProductID;
                product.ProductName = x.ProductName + " - " + x.Color + " - " + x.Size;
                lProductViewModel.Add(product);
            }

            cbProduct.ItemsSource = lProductViewModel.OrderBy(m => m.ProductName).ToList();
            cbProduct.DisplayMemberPath = "ProductName";
            cbProduct.SelectedValuePath = "ProductID";

            cbCategory.ItemsSource = db.Categories.OrderBy(m => m.ProductCategory).ToList();
            cbCategory.DisplayMemberPath = "ProductCategory";
            cbCategory.SelectedValuePath = "CategoryID";

        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            lOrderViewModel = new List<OrderViewModel>();

            int prodNumber = 1;
            //var purchases = db.GetPurchases().Where(m => m.SKUNumber == keyword).ToList();
            List<GetPurchases_Result> purchases = new List<GetPurchases_Result>();
            if (rbSelectAll.IsChecked==true)
            {
                purchases = db.GetPurchases().ToList();
            }
            else if (rbProduct.IsChecked==true)
            {
                var productid = Convert.ToInt32(cbProduct.SelectedValue);
                purchases = db.GetPurchases().Where(m=>m.ProductID==productid).ToList();
            }
            else if (rbCategory.IsChecked == true)
            {
                var categoryid = Convert.ToInt32(cbCategory.SelectedValue);
                purchases = db.GetPurchases().Where(m => m.CategoryID == categoryid).ToList();
            }
            else if (rbSkuNo.IsChecked==true)
            {
                var skuno = tbSKUNO.Text;
                purchases = db.GetPurchases().Where(m => m.SKUNumber == skuno).ToList();
            }

            if (purchases.Count() == 0)
            {
                MessageBox.Show("no product found");
                return;
            }
            foreach (var x in purchases)
            {
                OrderViewModel order = new OrderViewModel();
                order.ProductID = x.ProductID;
                order.Number = prodNumber;
                order.ProductName = x.ProductName;
                order.Color = x.Color;
                order.Size = x.ProductSize;
                order.Category = x.ProductCategory;
                order.Color = x.Color;
                order.RemainingQuantity = Convert.ToInt32(x.RemainingStock);
                order.Quantity = Convert.ToInt32(x.Quantity);
                order.Supplier = x.SupplierName;
                order.Price = Convert.ToInt32(x.Price);
                order.PurchaseDetailID = x.PurchaseDetailsID;
                order.Location = x.LocationName;
                order.Sales = x.TotalSales;
                order.Adjustment = x.Adjustment;
                order.SKUNo = x.SKUNumber;
                order.Location = x.LocationName;
                prodNumber++;
                lOrderViewModel.Add(order);
            }
            datagridview.ItemsSource = lOrderViewModel.ToList();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintWindow x = new PrintWindow();
            x.lOrderViewModel = lOrderViewModel.ToList();
            x.rptid = 1;
            x.Show();
            lOrderViewModel.Clear();
        }

        private void RbSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            cbCategory.IsEnabled = false;
            cbProduct.IsEnabled = false;
            tbSKUNO.IsEnabled = false;
        }

        private void RbSkuNo_Checked(object sender, RoutedEventArgs e)
        {
            tbSKUNO.Focus();
            cbCategory.IsEnabled = false;
            cbProduct.IsEnabled = false;
            tbSKUNO.IsEnabled = true;
        }

        private void RbProduct_Checked(object sender, RoutedEventArgs e)
        {
            cbProduct.Focus();
            cbCategory.IsEnabled = false;
            cbProduct.IsEnabled = true;
            tbSKUNO.IsEnabled = false;
        }

        private void RbCategory_Checked(object sender, RoutedEventArgs e)
        {
            cbCategory.Focus();
            cbCategory.IsEnabled = true;
            cbProduct.IsEnabled = false;
            tbSKUNO.IsEnabled = false;
        }
    }
}
