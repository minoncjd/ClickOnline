using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnline.Model
{
    public class PurchaseViewModel
    {
        public int PurchaseID { get; set; }
        public int Number { get; set; }
        public int? ProductID { get; set; }
        public string Location { get; set; }
        public int? LocationID { get; set; }
        public string ProductName { get; set; }
        public string SKUNo { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }

    }
}
