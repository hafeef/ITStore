using Core.Common.BaseTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("PurchaseOrderID:{PurchaseOrderID}, VendorID:{VendorID}, POTypeText:{POTypeText}, PoOrContractNumber:{PoOrContractNumber}, GrandTotal:{GrandTotal}, ReceivedTotal:{ReceivedTotal}")]
    public class PurchaseOrderVM : EntityBaseVM
    {
        public int PurchaseOrderID { get; set; }
        public int VendorID { get; set; }
        public int POTypeValue { get; set; }
        public string POTypeText { get; set; }
        public string PoOrContractNumber { get; set; }
        public double GrandTotal { get; set; }
        public double ReceivedTotal { get; set; }
        public DateTime POCreatedDate { get; set; }
        public List<PurchaseOrderLineItemVM> PurchaseOrderLineItems { get; set; }
        public List<ReceivedLineItemVM> ReceivedLineItems { get; set; }
    }
}
