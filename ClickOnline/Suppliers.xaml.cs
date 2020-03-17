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
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<SupplierViewModel> lSupplier = new List<SupplierViewModel>();
        public Suppliers()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SupplierAdd x = new SupplierAdd();
            x.ShowDialog();
        }

        private void GetSuppliers()
        {
            try
            {
                int number = 1;
                lSupplier = new List<SupplierViewModel>();
                var suppliers = db.Suppliers.OrderBy(m => m.SupplierName).ToList();

                foreach (var x in suppliers)
                {
                    SupplierViewModel supplier = new SupplierViewModel();
                    supplier.SupplierID = x.SupplierID;
                    supplier.Number = number;
                    supplier.SupplierName = x.SupplierName;
                    supplier.ContactNo = x.ContactNo;
                    supplier.ContactPerson = x.ContactPerson;
                    supplier.Address = x.Address;
                    number++;
                    lSupplier.Add(supplier);
                }
                datagridview.ItemsSource = lSupplier.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetSuppliers();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var x = ((SupplierViewModel)datagridview.SelectedItem);
            SupplierUpdate xx = new SupplierUpdate();
            xx.supplierid = x.SupplierID;
            xx.ShowDialog();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var keyword = tbSearch.Text;
                datagridview.ItemsSource = lSupplier.Where(m => m.SupplierName.Contains(keyword) || m.Address.Contains(keyword) || m.ContactPerson.Contains(keyword) || m.ContactNo.Contains(keyword)).ToList();
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text == "")
            {
                GetSuppliers();
            }
        }
    }
}
