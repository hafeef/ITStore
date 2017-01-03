using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ReceivedLineItemID:{ReceivedLineItemID}, PurchaseOrderID:{PurchaseOrderID}, ItemID:{ItemID}, Price:{Price}, Total:{Total}, SerialNo:{SerialNo}")]
    public class ReceivedLineItemsVM : EntityBaseVM
    {
        public int ReceivedLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public string SerialNo { get; set; }
    }
}
