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
    /// Interaction logic for SupplierAdd.xaml
    /// </summary>
    public partial class SupplierAdd : MetroWindow
    {

        ClickOnlineEntities db = new ClickOnlineEntities();
        public SupplierAdd()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
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
                    db.Suppliers.Add(x);
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Suppliers x = new Suppliers();
            x.ShowDialog();
        }
    }
}
