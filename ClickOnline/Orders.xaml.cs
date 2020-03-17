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
    public partial class Invoice : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<OrderViewModel> lCartOrderViewModel = new List<OrderViewModel>();
        public Invoice()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Now;
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<OrderViewModel> lOrderViewModel = new List<OrderViewModel>();
                var keyword = tbSearch.Text;
                int prodNumber = 1;
                var purchases = db.GetPurchases().Where(m => m.SKUNumber == keyword).ToList();

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
                    prodNumber++;
                    lOrderViewModel.Add(order);
                }
                datagridview.ItemsSource = lOrderViewModel.ToList();
            }

        }

        private void Datagridview_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var x = ((OrderViewModel)datagridview.SelectedItem);

            if (x != null)
            {
                tbItemCode.Text = x.ProductID.ToString() + " - " + x.ProductName;
                tbQuantity.Text = "";

                tbQuantity.Focus();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((OrderViewModel)datagridview.SelectedItem);
                if (tbQuantity.Text != "" && tbItemCode.Text != "")
                {
                    if (Convert.ToInt32(tbQuantity.Text) > x.RemainingQuantity)
                    {
                        MessageBox.Show("not enough stock");
                        return;
                    }

                    var ifexist = lCartOrderViewModel.Where(m => m.PurchaseDetailID == x.PurchaseDetailID).FirstOrDefault();

                    if (ifexist == null)
                    {
                        OrderViewModel xx = new OrderViewModel();
                        xx.OrderID = x.OrderID;
                        xx.ProductID = x.ProductID;
                        xx.ProductName = x.ProductName;
                        xx.SKUNo = x.SKUNo;
                        xx.Supplier = x.Supplier;
                        xx.Category = x.Category;
                        xx.Color = x.Color;
                        xx.Size = x.Size;
                        xx.Price = x.Price;
                        xx.Quantity = Convert.ToInt32(tbQuantity.Text);
                        xx.Total = Convert.ToInt32(x.Quantity * x.Price);
                        xx.Number = x.Number;
                        xx.PurchaseDetailID = x.PurchaseDetailID;
                        lCartOrderViewModel.Add(xx);
                        datagridviewCart.ItemsSource = lCartOrderViewModel.ToList();
                    }
                    else
                    {
                        MessageBox.Show("product already in the cart");
                    }
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
                if (tbCustomer.Text != "" && tbInvoice.Text != "" && dpDate.SelectedDate != null && lCartOrderViewModel.Count() != 0)
                {
                    Order order = new Order();
                    order.InvoiceNo = tbInvoice.Text;
                    order.Customer = tbCustomer.Text;
                    order.Date = dpDate.SelectedDate;
                    db.Orders.Add(order);

                    foreach (var x in lCartOrderViewModel)
                    {
                        order.OrderDetails.Add(new OrderDetail { PurchaseDetailID = x.PurchaseDetailID, Quantity = x.Quantity, Price = x.Price });
                    }

                    db.SaveChanges();

                    MessageBox.Show("successful");
                    tbSearch.Text = "";
                    tbItemCode.Text = "";
                    tbQuantity.Text = "";
                    tbInvoice.Text = "";
                    tbCustomer.Text = "";
                    dpDate.SelectedDate = DateTime.Now;
                    lCartOrderViewModel.Clear();
                    datagridview.ItemsSource = null;
                    datagridviewCart.ItemsSource = null;
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
            var x = ((OrderViewModel)datagridview.SelectedItem);
            lCartOrderViewModel.RemoveAll(m => m.PurchaseDetailID == x.PurchaseDetailID);
            datagridviewCart.ItemsSource = lCartOrderViewModel;
        }
    }
}
