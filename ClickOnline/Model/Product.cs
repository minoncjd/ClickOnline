//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClickOnline.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
            this.Inventories = new HashSet<Inventory>();
        }
    
        public int ProductID { get; set; }
        public string SKUNo { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<System.DateTime> GoodUntil { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string Location { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
