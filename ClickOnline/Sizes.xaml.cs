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
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Sizes : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        public Sizes()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SizeAdd x = new SizeAdd();
            x.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<SizeViewModel> lSize = new List<SizeViewModel>();
            var sizes = db.Sizes.OrderBy(m => m.ProductSize).ToList();
            int number = 1;
            foreach (var x in sizes)
            {
                SizeViewModel size = new SizeViewModel();
                size.SizeID = x.SizeID;
                size.Number = number;
                size.Size = x.ProductSize;
                number++;
                lSize.Add(size);
            }

            datagridview.ItemsSource = lSize.ToList();
        }
    }
}
