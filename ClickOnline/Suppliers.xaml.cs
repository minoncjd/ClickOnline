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
            try
            {
                if (tbSupplier.Text != "")
                {
                    Supplier x = new Supplier();
                    x.SupplierName = tbSupplier.Text;
                    x.ContactNo = tbContact.Text;
                    x.ContactPerson = tbContactPerson.Text;
                    x.Address = tbAddress.Text;
                    x.FaxNo = tbFax.Text;
                    x.Email = tbEmail.Text;
                    x.WhatsApp = tbWhatsApp.Text;
                    x.Website = tbWebsite.Text;
                    x.IsActive = true;
                    db.Suppliers.Add(x);
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    GetSuppliers();
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

        private void GetSuppliers()
        {
            try
            {
                int number = 1;
                lSupplier = new List<SupplierViewModel>();
                var suppliers = db.Suppliers.Where(m=>m.IsActive==true).OrderBy(m => m.SupplierName).ToList();

                foreach (var x in suppliers)
                {
                    SupplierViewModel supplier = new SupplierViewModel();
                    supplier.SupplierID = x.SupplierID;
                    supplier.Number = number;
                    supplier.SupplierName = x.SupplierName;
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

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnClear.IsEnabled = false;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var x = ((SupplierViewModel)datagridview.SelectedItem);

            try
            {
                if (tbSupplier.Text != "")
                {
                    var supplier = db.Suppliers.Where(m => m.SupplierID == x.SupplierID).FirstOrDefault();
                    supplier.SupplierName = tbSupplier.Text;
                    supplier.ContactPerson = tbContactPerson.Text;
                    supplier.ContactNo = tbContact.Text;
                    supplier.Address = tbAddress.Text;
                    supplier.FaxNo = tbFax.Text;
                    supplier.Email = tbEmail.Text;
                    supplier.WhatsApp = tbWhatsApp.Text;
                    supplier.Website = tbWebsite.Text;
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    GetSuppliers();
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

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            btnClear.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnAdd.IsEnabled = true;
            tbSupplier.Focus();
        }

        private void Clear()
        {
            tbContact.Text = "";
            tbContactPerson.Text = "";
            tbEmail.Text = "";
            tbFax.Text = "";
            tbSupplier.Text = "";
            tbWebsite.Text = "";
            tbWhatsApp.Text = "";
            tbAddress.Text = "";
        }

        private void Datagridview_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var x = ((SupplierViewModel)datagridview.SelectedItem);

            if (x != null)
            {
                var supplier = db.Suppliers.Where(m => m.SupplierID == x.SupplierID).FirstOrDefault();
                btnAdd.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnClear.IsEnabled = true;
                tbAddress.Text = supplier.Address;
                tbContact.Text = supplier.ContactNo;
                tbContactPerson.Text = supplier.ContactPerson;
                tbEmail.Text = supplier.Email;
                tbFax.Text = supplier.FaxNo;
                tbSupplier.Text = supplier.SupplierName;
                tbWebsite.Text = supplier.Website;
                tbWhatsApp.Text = supplier.WhatsApp;
            }
        }

        private void BtnDisable_Click(object sender, RoutedEventArgs e)
        {
            var x = ((SupplierViewModel)datagridview.SelectedItem);

            try
            {
                var supplier = db.Suppliers.Where(m => m.SupplierID == x.SupplierID).FirstOrDefault();
                supplier.IsActive = false;
                db.SaveChanges();
                MessageBox.Show("successful");
                GetSuppliers();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
