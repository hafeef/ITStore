using Core.Common.BaseTypes;
using System;

namespace Inventory.DomainClasses.Inventory
{
    public class InventoryIssue : EntityBase
    {
        public int InventoryIssueID { get; set; }
        public int EmployeeID { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
        public string HelpDeskTicket { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int ReceivedLineItemID { get; set; }
    }
}
