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
    /// Interaction logic for CategoryAdd.xaml
    /// </summary>
    public partial class CategoryAdd : MetroWindow
    {
        public CategoryAdd()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new ClickOnlineEntities())
                {
                    Category x = new Category();
                    x.ProductCategory = tbCategory.Text;
                    db.Categories.Add(x);
                    db.SaveChanges();
                    MessageBox.Show("successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Categories x = new Categories();
            x.ShowDialog();
        }
    }
}
