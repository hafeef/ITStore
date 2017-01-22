using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Inventory
{
    public class InventoryIssue : EntityBase
    {
        public int InventoryIssueID { get; set; }
        public int EmployeeID { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
        public string HelpDeskTicket { get; set; }
    }
}
