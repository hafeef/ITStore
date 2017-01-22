using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

namespace Inventory.Contracts.Inventory
{
    public interface IInventoryIssueRepository
    {
        List<InventoryIssueVM> FindInventoryIssueByEmployee(int employeeID);
    }
}
