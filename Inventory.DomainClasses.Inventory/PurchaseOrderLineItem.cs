using Core.Common.BaseTypes;
using System;

namespace Inventory.DomainClasses.Inventory
{
    [Serializable]
    public class PurchaseOrderLineItem : EntityBase
    {
        public int PurchaseOrderLineItemID { get; set; }
        public int PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string PartNumber { get; set; }
        public int PurchasedQuantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public int ReceivedQuantity { get; set; }
    }
}
