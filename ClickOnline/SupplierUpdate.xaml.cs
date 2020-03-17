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
    /// Interaction logic for SupplierUpdate.xaml
    /// </summary>
    public partial class SupplierUpdate : MetroWindow
    {
        public int supplierid;
        ClickOnlineEntities db = new ClickOnlineEntities();
        public SupplierUpdate()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                var supplier = db.Suppliers.Where(m => m.SupplierID == supplierid).FirstOrDefault();
                tbSupplier.Text = supplier.SupplierName;
                tbContact.Text = supplier.ContactNo;
                tbContactPerson.Text = supplier.ContactPerson;
                tbAddress.Text = supplier.Address;
                tbFax.Text = supplier.FaxNo;
                tbEmail.Text = supplier.Email;
                tbWhatsApp.Text = supplier.WhatsApp;
                tbWebsite.Text = supplier.Website;
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
                if (tbSupplier.Text != "")
                {
                    var supplier = db.Suppliers.Where(m => m.SupplierID == supplierid).FirstOrDefault();
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

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Suppliers x = new Suppliers();
            x.ShowDialog();
        }
    }
}
