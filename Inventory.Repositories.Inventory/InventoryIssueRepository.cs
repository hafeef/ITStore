using Inventory.Contracts.Inventory;
using System;
using System.Collections.Generic;
using Inventory.ViewModels.Inventory;
using Inventory.DomainClasses.Inventory;
using Inventory.Data.Inventory;
using System.Linq;
using System.Transactions;
using Core.Common.Resolvers;
using Core.Common.Enums;

namespace Inventory.Repositories.Inventory
{
    public class InventoryIssueRepository : DataRepositoryBase<InventoryIssue>, IInventoryIssueRepository, IDisposable
    {
        IAdministrationRepository _AdministrationRepository = new AdministrationRepository();
        public InventoryIssueRepository(InventoryContext context) : base(context)
        {
        }

        public List<InventoryIssueVM> FindInventoryIssueByEmployee(int employeeID)
        {
            try
            {
                var result = FindBy(ii => ii.EmployeeID == employeeID);
                var inventoryIssues = AutoMapper.Mapper.Map<IList<InventoryIssue>, IList<InventoryIssueVM>>(result);
                if (inventoryIssues.Count > 0)
                {
                    var itemIDs = inventoryIssues.Select(ii => ii.ItemID).Distinct().ToArray();
                    using (AdminReferenceContext context = new AdminReferenceContext())
                    {
                        var items = context.Items.Where(i => itemIDs.Contains(i.ItemID)).ToList();
                        return inventoryIssues.Join(items, ii => ii.ItemID, i => i.ItemID, (ii, i) => { ii.ItemDescription = i.Description; return ii; }).ToList();
                    }
                }
                else
                    return inventoryIssues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ReceivedLineItemVM FindReceivedLineItemBySerialNumber(string serialNo, int itemID)
        {
            using (InventoryContext context = new InventoryContext())
            {
                var result = context.ReceivedLineItems.FirstOrDefault(rli => rli.SerialNo == serialNo && rli.ItemID == itemID);
                return AutoMapper.Mapper.Map<ReceivedLineItemVM>(result);
            }
        }

        public InventoryIssueVM IsAssignToOtherEmployee(int itemID, string serialNo)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    var dbInventoryIssue = context.InventoryIssues.FirstOrDefault(ii => ii.SerialNo == serialNo && ii.ItemID == itemID && ii.IsReturned == false);
                    if (dbInventoryIssue != null)
                    {
                        var result = AutoMapper.Mapper.Map<InventoryIssueVM>(dbInventoryIssue);
                        using (AdminReferenceContext administrationContext = new AdminReferenceContext())
                        {
                            var employee = administrationContext.Employees.FirstOrDefault(e => e.EmployeeID == result.EmployeeID);
                            result.EmployeeName = employee.Name;
                            result.CivilID = employee.CivilID;
                        }
                        return result;
                    }
                    else
                        return null;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void SaveInventoryIssue(IEnumerable<InventoryIssueVM> inventoryIssues)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    var dbInventoryIssues = AutoMapper.Mapper.Map<IEnumerable<InventoryIssue>>(inventoryIssues);
                    using (InventoryContext context = new InventoryContext())
                    {
                        foreach (var inventoryIssue in dbInventoryIssues.Where(ii => ii.EntityState != ObjectState.Unchanged))
                            context.Entry(inventoryIssue).State = StateResolver.Resolve(inventoryIssue.EntityState);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }


}
