using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnline.Model
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int Number { get; set; }
        public int ProductID { get; set; }
        public int PurchaseDetailID { get; set; }
        public string ProductName { get; set; }
        public string SKUNo { get; set; }
        public string Location { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }
        public int Adjustment { get; set; }
        public int Sales { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
