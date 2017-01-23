using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

namespace Inventory.Contracts.Inventory
{
    public interface IInventoryIssueRepository
    {
        List<InventoryIssueVM> FindInventoryIssueByEmployee(int employeeID);
        ReceivedLineItemVM FindReceivedLineItemBySerialNumber(string serialNo, int itemID);
        InventoryIssueVM IsAssignToOtherEmployee(int itemID, string serialNo);
        void SaveInventoryIssue(IEnumerable<InventoryIssueVM> inventoryIssues);
    }
}
