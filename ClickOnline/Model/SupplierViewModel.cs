using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickOnline.Model
{
    class SupplierViewModel
    {
        public int SupplierID { get; set; }
        public int Number { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }

        public string WhatsApp { get; set; }
        public string Fax { get; set; }
    }
}
