using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnline.Model
{
    class ProductViewModel
    {
        public int ProductID { get; set; }
        public int Number { get; set; }
        public string SKUNo { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? PurchasePrice { get; set; }

        public byte[] Image1 { get; set; }

    }
}
