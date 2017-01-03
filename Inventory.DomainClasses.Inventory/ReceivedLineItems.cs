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
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public string SerialNo { get; set; }
    }
}
