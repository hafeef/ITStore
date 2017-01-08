using Core.Common.BaseTypes;
using System;
using System.Collections.Generic;

namespace Inventory.DomainClasses.Inventory
{
    [Serializable]
    public class PurchaseOrder : EntityBase
    {
        public int PurchaseOrderID { get; set; }
        public int VendorID { get; set; }
        public int POTypeValue { get; set; }
        public string POTypeText { get; set; }
        public string PoOrContractNumber { get; set; }
        public double GrandTotal { get; set; }
        public double ReceivedTotal { get; set; }
        public DateTime POCreatedDate { get; set; }
        public IList<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public IList<ReceivedLineItem> ReceivedLineItems { get; set; }
    }
}
