using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{InventoryIssueID}, ReceivedLineItemID:{ReceivedLineItemID}, EmployeeID:{EmployeeID}, ItemID:{ItemID}, ItemDescription:{ItemDescription}, SerialNo:{SerialNo}, HelpDeskTicket:{HelpDeskTicket}, IsReturned:{IsReturned}")]
    public class InventoryIssueVM : EntityBaseVM
    {
        public int InventoryIssueID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string CivilID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string SerialNo { get; set; }
        public string HelpDeskTicket { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int ReceivedLineItemID { get; set; }
    }
}
