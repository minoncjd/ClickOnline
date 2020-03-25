using ClickOnline.Model;
using CrystalDecisions.CrystalReports.Engine;
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
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : MetroWindow
    {
        public int rptid;
        public DateTime startdate;
        public DateTime enddate;
        ReportDocument report;

        public List<GetProductList_Result> lProduct = new List<GetProductList_Result>();
        public List<GetOrders_Result> lSales = new List<GetOrders_Result>();
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            crViewer1.Owner = Window.GetWindow(this);

            if (rptid==1)
            {
                if (lProduct.Count > 0)
                {
                    report = new Reports.InventoryReport();
                    report.SetDataSource(lProduct.ToList()); ;
                    crViewer1.ViewerCore.ReportSource = report;
                }

            }
            else if (rptid == 2)
            {
                report = new Reports.Sales();
                report.SetDataSource(lSales.ToList());
                report.SetParameterValue("startdate", startdate);
                report.SetParameterValue("enddate", enddate);
                crViewer1.ViewerCore.ReportSource = report;
            }
        }
    }
}
