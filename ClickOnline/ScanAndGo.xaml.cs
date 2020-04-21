using ClickOnline.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ScanAndGo : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        GetProducts_Result product = new GetProducts_Result();
        public ScanAndGo()
        {
            InitializeComponent();
        }

       
    
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rbIn.IsChecked = true;
            pic.Source = null;
        }

        private void TbSku_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product.SKUNo == null || product.SKUNo == "" || tbQuantity.Text == "")
                {
                    MessageBox.Show("complete the required data");
                    return;
                }
               
                if (rbOut.IsChecked==true)
                {
                    if (Convert.ToInt32(tbQuantity.Text) > Convert.ToInt32(tbRemaningQuantity.Text))
                    {
                        MessageBox.Show("not enought stocks");
                        return;
                    }
                }
                    
                Inventory x = new Inventory();
                x.ProductID = product.ProductID;
                if (rbIn.IsChecked==true)
                {
                    x.Quantity = Convert.ToInt32(tbQuantity.Text);
                }
                else if (rbOut.IsChecked == true)
                {
                    x.Quantity = Convert.ToInt32("-"+tbQuantity.Text);
                }
                   
                x.Date = DateTime.Now;
                db.Inventories.Add(x);
                db.SaveChanges();
                MessageBox.Show("successful");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        private void Clear()
        {   
            tbProductName.Text = "";
            tbSKU.Text = "";
            tbDescription.Text = "";
            tbColor.Text = "";
            tbSize.Text = "";
            tbPurchasePrice.Text = "";
            tbLocation.Text = "";
            tbQuantity.Text = "";
            tbSupplier.Text = "";
            tbCategory.Text = "";
            dpGoodUntil.SelectedDate = null;
            tbSellingPrice.Text = "";
            tbTax.Text = "";
            tbRemaningQuantity.Text = "";

            tbSku.Text = "";
            tbQuantity.Text = "";
            rbIn.IsChecked = true;
            pic.Source = null;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {
                    var skuno = tbSku.Text.ToUpper();
                    product = db.GetProducts(skuno).FirstOrDefault();
                    if (product != null)
                    {
                        tbSKU.Text = product.SKUNo;
                        tbProductName.Text = product.ProductName;
                        tbDescription.Text = product.Description;
                        tbColor.Text = product.Color;
                        tbLocation.Text = product.Location;
                        tbPurchasePrice.Text = Convert.ToDecimal(product.PurchasePrice).ToString("C", new CultureInfo("en-US"));
                        tbRemaningQuantity.Text = product.Quantity.ToString();
                        tbSellingPrice.Text = Convert.ToDecimal(product.SellingPrice).ToString("C", new CultureInfo("en-US"));
                        tbSize.Text = product.Size;
                        tbTax.Text = product.Tax.ToString();
                        dpGoodUntil.SelectedDate = product.GoodUntil;
                        tbCategory.Text = product.ProductCategory;
                        tbSupplier.Text = product.SupplierName;

                        if (product.Image1 != null)
                        {
                            pic.Source = LoadImage(product.Image1);
                        }
                        else
                        {
                            var uriSource = new Uri(@"/ClickOnline;component/Images/noimage.png", UriKind.Relative);
                            pic.Source = new BitmapImage(uriSource);
                        }
                    }
                    else
                    {
                        Clear();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

    }
}
