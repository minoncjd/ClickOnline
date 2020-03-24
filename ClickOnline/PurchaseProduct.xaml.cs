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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class PurchaseProduct : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<PurchaseViewModel> lPurchaseViewModel = new List<PurchaseViewModel>();
        List<ProductViewModel> lProductViewModel = new List<ProductViewModel>();
        public PurchaseProduct()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Now;

            lProductViewModel = new List<ProductViewModel>();
            var products = db.GetProductList().ToList();
            foreach (var x in products)
            {
                ProductViewModel product = new ProductViewModel();
                product.ProductID = x.ProductID;
                product.ProductName = x.ProductName + " - " + x.Color + " - " + x.Size;
                lProductViewModel.Add(product);
            }

            cbItem.ItemsSource = lProductViewModel.OrderBy(m => m.ProductName).ToList();
            cbItem.DisplayMemberPath = "ProductName";
            cbItem.SelectedValuePath = "ProductID";

            cbSupplier.ItemsSource = db.Suppliers.OrderBy(m => m.SupplierName).ToList();
            cbSupplier.DisplayMemberPath = "SupplierName";
            cbSupplier.SelectedValuePath = "SupplierID";

            cbLocation.ItemsSource = db.Locations.OrderBy(m => m.LocationName).ToList();
            cbLocation.DisplayMemberPath = "LocationName";
            cbLocation.SelectedValuePath = "LocationID";

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = 1;

                if (cbItem.Text != "" && tbQuantity.Text != "" && tbSKU.Text != "" && cbLocation.Text != "")
                {
                    var productid = Convert.ToInt32(cbItem.SelectedValue);
                    var product = db.Products.Where(m => m.ProductID == productid).First();
                    PurchaseViewModel purchase = new PurchaseViewModel();
                    purchase.ProductID = product.ProductID;
                    purchase.Number = number;
                    purchase.ProductName = product.ProductName;
                    purchase.Category = product.Category.ProductCategory;
                    purchase.Color = product.Color;
                    purchase.Price = product.PurchasePrice;
                    purchase.Total = Convert.ToInt32(tbQuantity.Text) * product.PurchasePrice;
                    purchase.Quantity = Convert.ToInt32(tbQuantity.Text);
                    purchase.SKUNo = tbSKU.Text;
                    purchase.LocationID = Convert.ToInt32(cbLocation.SelectedValue);
                    purchase.Location = cbLocation.Text;
                    number++;
                    lPurchaseViewModel.Add(purchase);

                    datagridview.ItemsSource = lPurchaseViewModel.ToList();
                    cbItem.Focus();
                    tbQuantity.Text = "";
                    tbSKU.Text = "";
                    cbLocation.Text = "";
                }
                else
                {
                    MessageBox.Show("complete the required data");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbInvoiceNo.Text != "" && cbSupplier.Text != "" && dpDate.SelectedDate != null && lPurchaseViewModel.Count() != 0)
                {
                    Purchase purchase = new Purchase();
                    purchase.SupplierID = Convert.ToInt32(cbSupplier.SelectedValue);
                    purchase.InvoiceNo = tbInvoiceNo.Text;
                    purchase.Date = dpDate.SelectedDate;
                    db.Purchases.Add(purchase);

                    foreach (var x in lPurchaseViewModel)
                    {
                        purchase.PurchaseDetails.Add(new PurchaseDetail() { SKUNumber = x.SKUNo, ProductID = x.ProductID, Quantity = x.Quantity, Price = x.Price, LocationID = x.LocationID });
                    }

                    db.SaveChanges();
                    MessageBox.Show("successful");
                    lPurchaseViewModel.Clear();
                    datagridview.ItemsSource = lPurchaseViewModel.ToList();
                    cbItem.Text = "";
                    tbQuantity.Text = "";
                    cbSupplier.Text = "";
                    tbInvoiceNo.Text = "";
                    tbSKU.Text = "";
                    cbLocation.Text = "";
                    dpDate.SelectedDate = DateTime.Now;

                }
                else
                {
                    MessageBox.Show("complete the required data");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var x = ((PurchaseViewModel)datagridview.SelectedItem);
            lPurchaseViewModel.RemoveAll(m => m.Number == x.Number);
            datagridview.ItemsSource = lPurchaseViewModel;
        }
    }
}
