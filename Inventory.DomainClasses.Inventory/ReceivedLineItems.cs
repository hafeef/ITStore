using Core.Common.BaseTypes;
using System;

namespace Inventory.DomainClasses.Inventory
{
    [Serializable]
    public class ReceivedLineItem : EntityBase
    {
        public int ReceivedLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public int PurchaseOrderLineItemID { get; set; }
        public PurchaseOrderLineItem PurchaseOrderLineItem { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string PartNumber { get; set; }
        public string SerialNo { get; set; }
        public int WareHouseID { get; set; }
        public int ShelfID { get; set; }
        public int RackID { get; set; }
        public DateTime WarrantyDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
