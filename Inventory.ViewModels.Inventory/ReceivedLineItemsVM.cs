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
        public string ItemDescription { get; set; }
        public string SerialNo { get; set; }
        public string PartNumber { get; set; }
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public int RackID { get; set; }
        public string RackName { get; set; }
        public int ShelfID { get; set; }
        public string ShelfName { get; set; }
        public DateTime WarrantyDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (ReferenceEquals(obj, this))
                return true;
            else if (GetType() != obj.GetType())
                return false;
            var receivedLineItem = obj as ReceivedLineItemVM;

            return SerialNo == receivedLineItem.SerialNo && PartNumber == receivedLineItem.PartNumber;

            //return PurchaseOrderID == receivedLineItem.PurchaseOrderID &&
            //       PurchaseOrderLineItemID == receivedLineItem.PurchaseOrderLineItemID &&
            //       ReceivedLineItemID == receivedLineItem.ReceivedLineItemID &&
            //       SerialNo == receivedLineItem.SerialNo && WarehouseID == receivedLineItem.WarehouseID &&
            //       RackID == receivedLineItem.RackID && ShelfID == receivedLineItem.ShelfID &&
            //       ReceivedDate == receivedLineItem.ReceivedDate && WarrantyDate == receivedLineItem.WarrantyDate &&
            //       ExpiryDate == receivedLineItem.ExpiryDate;
        }

        public override int GetHashCode()
        {
            return SerialNo.GetHashCode() ^ PartNumber.GetHashCode();
        }

        public static bool operator ==(ReceivedLineItemVM lsh, ReceivedLineItemVM rsh)
        {
            return Equals(lsh, rsh);
        }

        public static bool operator !=(ReceivedLineItemVM lsh, ReceivedLineItemVM rsh)
        {
            return !Equals(lsh, rsh);
        }
    }
}
