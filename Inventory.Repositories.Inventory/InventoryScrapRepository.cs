using Inventory.Contracts.Inventory;
using Inventory.DomainClasses.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data.Inventory;
using Inventory.ViewModels.Inventory;
using System.Transactions;
using Core.Common.Resolvers;

namespace Inventory.Repositories.Inventory
{
    public class InventoryScrapRepository : DataRepositoryBase<InventoryScrap>, IInventoryScrapRepository
    {
        public InventoryScrapRepository(InventoryContext context) : base(context)
        {

        }

        public void SaveInventoryScrap(IEnumerable<InventoryScrapVM> inventoryScraps)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (InventoryContext context = new InventoryContext())
                    {
                        var dbInventoryScraps = AutoMapper.Mapper.Map<IEnumerable<InventoryScrap>>(inventoryScraps);
                        foreach (var scrapItem in dbInventoryScraps)
                            context.Entry(scrapItem).State = StateResolver.Resolve(scrapItem.EntityState);
                        context.SaveChanges();
                    }
                    scope.Complete();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public HashSet<InventoryScrapVM> GetAllInventoryScraps()
        {
            try
            {
                var inventoryScraps = AutoMapper.Mapper.Map<IEnumerable<InventoryScrap>, IEnumerable<InventoryScrapVM>>(All());
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var itemIDs = inventoryScraps.Select(ins => ins.ItemID).Distinct().ToArray();
                    var items = context.Items.Where(i => itemIDs.Contains(i.ItemID)).ToList();
                    return new HashSet<InventoryScrapVM>(inventoryScraps.Join(items, ins => ins.ItemID, i => i.ItemID, (ins, i) => { ins.ItemDescription = i.Description; return ins; }));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
