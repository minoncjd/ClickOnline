﻿using ClickOnline.Model;
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
    /// Interaction logic for ProductUpdate.xaml
    /// </summary>
    public partial class ProductUpdate : MetroWindow
    {
        public int productid;
        ClickOnlineEntities db = new ClickOnlineEntities();
        public ProductUpdate()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {            
                cbCategory.ItemsSource = db.Categories.OrderBy(m => m.ProductCategory).ToList();
                cbCategory.DisplayMemberPath = "ProductCategory";
                cbCategory.SelectedValuePath = "CategoryID";

                cbSize.ItemsSource = db.Sizes.OrderBy(m => m.ProductSize).ToList();
                cbSize.DisplayMemberPath = "ProductSize";
                cbSize.SelectedValuePath = "SizeID";

                var product = db.Products.Where(m => m.ProductID == productid).FirstOrDefault();
                tbName.Text = product.ProductName;
                cbCategory.SelectedValue = product.CategoryID;
                tbColor.Text = product.Color;
                tbSellingPrize.Text = product.SellingPrice.ToString();
                tbPurchasePrize.Text = product.PurchasePrice.ToString();

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
                if (tbName.Text != "" && tbColor.Text != "" && cbCategory.Text != "" && cbSize.Text != "" && tbSellingPrize.Text != "" && tbPurchasePrize.Text != "")
                {
                    var product = db.Products.Where(m => m.ProductID == productid).FirstOrDefault();
                    product.ProductName = tbName.Text;
                    product.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                    product.Color = tbColor.Text;
                    product.SellingPrice = Convert.ToInt32(tbSellingPrize.Text);
                    product.PurchasePrice = Convert.ToInt32(tbPurchasePrize.Text);
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
            //Products x = new Products();
            //x.ShowDialog();
        }
    }
}
