using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ReceivedLineItemID:{ReceivedLineItemID}, PurchaseOrderID:{PurchaseOrderID}, ItemID:{ItemID}, PurchaseOrderLineItemID:{PurchaseOrderLineItemID}, ReceivedDate:{ReceivedDate}, SerialNo:{SerialNo}, WarehouseID:{WarehouseID}, RackID:{RackID}, ShelfID:{ShelfID},")]
    public class ReceivedLineItemVM : EntityBaseVM
    {
        public int ReceivedLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int PurchaseOrderLineItemID { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
        public int WarehouseID { get; set; }
        public int RackID { get; set; }
        public int ShelfID { get; set; }
    }
}
