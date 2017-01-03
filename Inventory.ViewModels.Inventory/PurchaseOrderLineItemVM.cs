using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("PurchaseOrderID:{PurchaseOrderID}, LineItemID:{PurchaseOrderLineItemID}, ItemID:{ItemID}, ItemDescription:{ItemDescription}, Quantity:{Quantity}, Price:{Price}, Total:{Total}")]
    public class PurchaseOrderLineItemVM : EntityBaseVM
    {
        public int SrNo { get; set; }
        public int PurchaseOrderLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string PartNumber { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
