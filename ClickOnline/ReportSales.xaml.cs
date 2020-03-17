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
    /// Interaction logic for ReportInventory.xaml
    /// </summary>
    public partial class ReportSales : MetroWindow
    {
        ClickOnlineEntities db = new ClickOnlineEntities();
        List<GetOrders_Result> lOrders = new List<GetOrders_Result>();
        public ReportSales()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var startdate = dpStartDate.SelectedDate.Value.Date;
            var enddate = dpEndDate.SelectedDate.Value.Date;
            var sales = db.GetOrders(startdate, enddate).ToList();
          
            lOrders.AddRange(sales);

            PrintWindow x = new PrintWindow();
            x.lSales = lOrders.ToList();
            x.rptid = 2;
            x.startdate = startdate;
            x.enddate = enddate;
            x.Show();
            lOrders.Clear();

        }
    }
}
