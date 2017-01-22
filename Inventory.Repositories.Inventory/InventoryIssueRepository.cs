using Inventory.Contracts.Inventory;
using System;
using System.Collections.Generic;
using Inventory.ViewModels.Inventory;
using Inventory.DomainClasses.Inventory;
using Inventory.Data.Inventory;

namespace Inventory.Repositories.Inventory
{
    public class InventoryIssueRepository : DataRepositoryBase<InventoryIssue>, IInventoryIssueRepository
    {
        public InventoryIssueRepository(InventoryContext context) : base(context)
        {
        }

        public List<InventoryIssueVM> FindInventoryIssueByEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }
    }


}
