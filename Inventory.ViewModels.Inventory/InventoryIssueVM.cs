using Core.Common.BaseTypes;

namespace Inventory.ViewModels.Inventory
{
    public class InventoryIssueVM : EntityBaseVM
    {
        public int InventoryIssueID { get; set; }
        public int EmployeeID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string SerialNo { get; set; }
        public string HelpDeskTicket { get; set; }
    }
}
