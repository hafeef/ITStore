using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("PurchaseOrderID:{PurchaseOrderID}, LineItemID:{PurchaseOrderLineItemID}, ItemID:{ItemID}, ItemDescription:{ItemDescription}, Quantity:{PurchasedQuantity}, Price:{Price}, Total:{Total}, ReceivedQuatity:{ReceivedQuatity}")]
    public class PurchaseOrderLineItemVM : EntityBaseVM
    {
        public int SrNo { get; set; }
        public int PurchaseOrderLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string PartNumber { get; set; }
        public int PurchasedQuantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public int ReceivedQuantity { get; set; }
        public int RemainingQuantity { get { return PurchasedQuantity - ReceivedQuantity; } }
    }
}
