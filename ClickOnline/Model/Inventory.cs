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
    
    public partial class Inventory
    {
        public int InventoryID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
