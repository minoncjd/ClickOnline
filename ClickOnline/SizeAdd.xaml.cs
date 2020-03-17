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
    public partial class SizeAdd : MetroWindow
    {
        public SizeAdd()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new ClickOnlineEntities())
                {
                    if (tbSize.Text != "")
                    {
                        Model.Size x = new Model.Size();
                        x.ProductSize = tbSize.Text;
                        db.Sizes.Add(x);
                        db.SaveChanges();
                        MessageBox.Show("successful");
                    }
                    else
                    {
                        MessageBox.Show("complete the required data");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Sizes x = new Sizes();
            x.ShowDialog();
        }
    }
}
