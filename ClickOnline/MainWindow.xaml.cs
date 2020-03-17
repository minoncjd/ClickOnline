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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClickOnline
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mahTileProduct_Click(object sender, RoutedEventArgs e)
        {
            Products x = new Products();
            x.ShowDialog();
        }

        private void mahTileCategory_Click(object sender, RoutedEventArgs e)
        {
            Categories x = new Categories();
            x.ShowDialog();
        }

        private void mahTileSupplier_Click(object sender, RoutedEventArgs e)
        {
            Suppliers x = new Suppliers();
            x.ShowDialog();
        }

        private void mahTileSize_Click(object sender, RoutedEventArgs e)
        {
            Sizes x = new Sizes();
            x.ShowDialog();
        }

        private void mahInvoice_Click(object sender, RoutedEventArgs e)
        {
            Invoice x = new Invoice();
            x.Show();
        }

        private void mahPurchase_Click(object sender, RoutedEventArgs e)
        {
            PurchaseProduct x = new PurchaseProduct();
            x.ShowDialog();
        }

        private void mahAdjustment_Click(object sender, RoutedEventArgs e)
        {
            Adjustment x = new Adjustment();
            x.ShowDialog();
        }

        private void MahInventory_Click(object sender, RoutedEventArgs e)
        {
            ReportInventory x = new ReportInventory();
            x.ShowDialog();
        }

        private void MahSales_Click(object sender, RoutedEventArgs e)
        {
            ReportSales x = new ReportSales();
            x.ShowDialog();
        }

        private void MahPurchases_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
