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
        List<GetProductList_Result> lProduct = new List<GetProductList_Result>();

        public ReportInventory()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cbCategory.ItemsSource = db.Categories.OrderBy(m => m.ProductCategory).ToList();
            cbCategory.DisplayMemberPath = "ProductCategory";
            cbCategory.SelectedValuePath = "CategoryID";
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            List<GetProductList_Result> products = new List<GetProductList_Result>();
            if (rbCategory.IsChecked ==true)
            {
                var cat = cbCategory.Text;
                products = db.GetProductList().Where(m=>m.ProductCategory == cat).ToList();
            }
            else if (rbSkuNo.IsChecked==true)
            {
                var sku = tbSKUNO.Text;
                products = db.GetProductList().Where(m => m.SKUNo == sku).ToList();
            }
            else if (rbSelectAll.IsChecked==true)
            {
                products = db.GetProductList().ToList();
            }
           
            lProduct = new List<GetProductList_Result>();
            
            foreach (var x in products)
            {
                GetProductList_Result y = new GetProductList_Result();
                y.ProductID = x.ProductID;
                y.ProductName = x.ProductName;
                y.Color = x.Color;
                y.Description = x.Description;
                y.GoodUntil = x.GoodUntil;
                y.Location = x.Location;
                y.ProductCategory = x.ProductCategory;
                y.PurchasePrice = x.PurchasePrice;
                y.Quantity = x.Quantity;
                y.SellingPrice = x.SellingPrice;
                y.Size = x.Size;
                y.SKUNo = x.SKUNo;
                y.SupplierName = x.SupplierName;
                y.Tax = x.Tax;
                lProduct.Add(y);
    
            }
            datagridview.ItemsSource = lProduct.ToList();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintWindow x = new PrintWindow();
            x.lProduct = lProduct.ToList();
            x.rptid = 1;
            x.Show();
            lProduct.Clear();
        }

        private void RbSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            cbCategory.IsEnabled = false;
            tbSKUNO.IsEnabled = false;
        }

        private void RbSkuNo_Checked(object sender, RoutedEventArgs e)
        {
            tbSKUNO.Focus();
            cbCategory.IsEnabled = false;
            tbSKUNO.IsEnabled = true;
        }

   
        private void RbCategory_Checked(object sender, RoutedEventArgs e)
        {
            cbCategory.Focus();
            cbCategory.IsEnabled = true;
            tbSKUNO.IsEnabled = false;
        }
    }
}
