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
    
    public partial class PurchaseDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseDetail()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Adjustments = new HashSet<Adjustment>();
        }
    
        public int PurchaseDetailsID { get; set; }
        public Nullable<int> PurchaseID { get; set; }
        public string SKUNumber { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual Purchase Purchase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjustment> Adjustments { get; set; }
        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
