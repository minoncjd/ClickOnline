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
    /// Interaction logic for Adjustment.xaml
    /// </summary>
    public partial class Adjustment : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        public Adjustment()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((OrderViewModel)datagridview.SelectedItem);
                Model.Adjustment adj = new Model.Adjustment();
                adj.PurchaseDetailsID = x.PurchaseDetailID;
                adj.Quantity = Convert.ToInt32(tbQuantity.Text);
                adj.AdjustmentDate = DateTime.Now;
                db.Adjustments.Add(adj);
                db.SaveChanges();
                MessageBox.Show("successful");
                datagridview.ItemsSource = null;
                tbQuantity.Text = "";
                tbItemCode.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    order.PurchaseDetailID = x.ProductID;
                    order.ProductID = x.ProductID;
                    order.Number = prodNumber;
                    order.ProductName = x.ProductName;
                    order.Color = x.Color;
                    order.Size = x.ProductSize;
                    order.Category = x.ProductCategory;
                    order.Color = x.Color;
                    order.RemainingQuantity = Convert.ToInt32(x.RemainingStock);
                    order.Quantity = Convert.ToInt32(x.Quantity);
                    order.Adjustment = x.Adjustment;
                    order.Sales = x.TotalSales;
                    order.Supplier = x.SupplierName;
                    order.Price = Convert.ToInt32(x.Price);
                    order.PurchaseDetailID = x.PurchaseDetailsID;
                    order.Location = x.LocationName;
                    prodNumber++;
                    lOrderViewModel.Add(order);
                }
                datagridview.ItemsSource = lOrderViewModel.ToList();
            }
        }
    }
}
