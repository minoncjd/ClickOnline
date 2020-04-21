using ClickOnline.Model;
using MahApps.Metro.Controls;
using Microsoft.Win32;
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
    public partial class Products : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<ProductViewModel> lProduct = new List<ProductViewModel>();
        public Products()
        {
            InitializeComponent();
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
            cbSupplier.Text = "";
            cbCategory.Text = "";
            dpGoodUntil.SelectedDate = null;
            tbSellingPrice.Text = "";
            tbTax.Text = "";
            pic.Source = null;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbProductName.Text != "" && tbSKU.Text != "" && tbQuantity.Text != "" && tbSellingPrice.Text != "" && tbPurchasePrice.Text != "" && cbCategory.Text != "" && cbSupplier.Text != "")
                {
                    Product x = new Product();
                    x.ProductName = tbProductName.Text;
                    x.SKUNo = tbSKU.Text;
                    x.Description = tbDescription.Text;
                    x.Color = tbColor.Text;
                    x.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                    x.Size = tbSize.Text;
                    x.SellingPrice = Convert.ToDecimal(tbSellingPrice.Text);
                    x.PurchasePrice = Convert.ToDecimal(tbPurchasePrice.Text);
                    x.GoodUntil = dpGoodUntil.SelectedDate;
                    x.Tax = tbTax.Text == "" ? (decimal?)null : Convert.ToDecimal(tbTax.Text);
                    x.Location = tbLocation.Text;
                    x.Quantity = Convert.ToInt32(tbQuantity.Text);
                    x.SupplierID = Convert.ToInt32(cbSupplier.SelectedValue);

                    if (!String.IsNullOrEmpty(txtPic.Text))
                    {
                        x.Image1 = File.ReadAllBytes(txtPic.Text);
                    }

                    db.Products.Add(x);
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    Clear();
                    GetProducts();
                }
                else
                {
                    MessageBox.Show("complete the required fields");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void GetProducts()
        {
            try
            {
                lProduct = new List<ProductViewModel>();
                var products = db.GetProductList().OrderBy(m => m.ProductName).ToList();

                foreach (var x in products)
                {
                    ProductViewModel product = new ProductViewModel();
                    product.ProductID = x.ProductID;
                    product.SKUNo = x.SKUNo;
                    product.ProductName = x.ProductName;
                    product.Image1 = x.Image1;
                    lProduct.Add(product);
                }
                datagridview.ItemsSource = lProduct.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetProducts();

            cbSupplier.ItemsSource = db.Suppliers.OrderBy(m => m.SupplierName).ToList();
            cbSupplier.DisplayMemberPath = "SupplierName";
            cbSupplier.SelectedValuePath = "SupplierID";

            cbCategory.ItemsSource = db.Categories.OrderBy(m => m.ProductCategory).ToList();
            cbCategory.DisplayMemberPath = "ProductCategory";
            cbCategory.SelectedValuePath = "CategoryID";

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnClear.IsEnabled = false;
        }

        private void Datagridview_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var x = ((ProductViewModel)datagridview.SelectedItem);
          
            if (x != null)
            {
                var prod = db.Products.Where(m => m.ProductID == x.ProductID).FirstOrDefault();
                btnAdd.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnClear.IsEnabled = true;
                tbSKU.Text = prod.SKUNo;
                tbColor.Text = prod.Color;
                tbDescription.Text = prod.Description;
                tbLocation.Text = prod.Location;
                tbProductName.Text = prod.ProductName;
                tbPurchasePrice.Text = Convert.ToDecimal(prod.PurchasePrice).ToString("C", new CultureInfo("en-US"));
                tbQuantity.Text = prod.Quantity.ToString();
                tbSellingPrice.Text = Convert.ToDecimal(prod.SellingPrice).ToString("C", new CultureInfo("en-US"));
                tbSize.Text = prod.Size;
                tbTax.Text = prod.Tax.ToString();
                dpGoodUntil.SelectedDate = prod.GoodUntil;
                cbCategory.SelectedValue = prod.CategoryID;
                cbSupplier.SelectedValue = prod.SupplierID;

                if (x.Image1 != null)
                {
                    pic.Source = LoadImage(x.Image1);
                }
                else
                {
                    var uriSource = new Uri(@"/ClickOnline;component/Images/noimage.png", UriKind.Relative);
                    pic.Source = new BitmapImage(uriSource);
                }
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
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((ProductViewModel)datagridview.SelectedItem);
                var prod = db.Products.Where(m => m.ProductID == x.ProductID).FirstOrDefault();

                if (tbProductName.Text != "" && tbSKU.Text != "" && tbQuantity.Text != "" && tbSellingPrice.Text != "" && tbPurchasePrice.Text != "" && cbCategory.Text != "" && cbSupplier.Text != "")
                {
                    prod.ProductName = tbProductName.Text;
                    prod.SKUNo = tbSKU.Text;
                    prod.Description = tbDescription.Text;
                    prod.Color = tbColor.Text;
                    prod.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                    prod.Size = tbSize.Text;
                    prod.SellingPrice = Convert.ToDecimal(tbSellingPrice.Text);
                    prod.PurchasePrice = Convert.ToDecimal(tbPurchasePrice.Text);
                    prod.GoodUntil = dpGoodUntil.SelectedDate;
                    prod.Tax = tbTax.Text == "" ? (decimal?)null : Convert.ToDecimal(tbTax.Text);
                    prod.Location = tbLocation.Text;
                    prod.Quantity = Convert.ToInt32(tbQuantity.Text);
                    prod.SupplierID = Convert.ToInt32(cbSupplier.SelectedValue);
                    if (!String.IsNullOrEmpty(txtPic.Text))
                    {
                        prod.Image1 = File.ReadAllBytes(txtPic.Text);
                    }
                    db.SaveChanges();
                    MessageBox.Show("successful");
                    Clear();
                    GetProducts();
                }
                else
                {
                    MessageBox.Show("complete the required fields");
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
            tbSKU.Focus();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            var keyword = tbSearch.Text;
            if (tbSearch.Text == "")
            {
                GetProducts();
            }
            datagridview.ItemsSource = lProduct.Where(m => m.SKUNo.Contains(keyword)).ToList();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.InitialDirectory = @"Desktop";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";


            if (op.ShowDialog() == true)
            {
                txtPic.Text = op.FileName;

                ImageSource imageSource = new BitmapImage(new Uri(txtPic.Text));
                pic.Source = imageSource;

            }
        }
    }
}
