using Inventory.Contracts.Inventory;
using Inventory.DomainClasses.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data.Inventory;
using Inventory.ViewModels.Inventory;

namespace Inventory.Repositories.Inventory
{
    public class InventoryScrapRepository : DataRepositoryBase<InventoryScrap>, IInventoryScrapRepository
    {
        public InventoryScrapRepository(InventoryContext context) : base(context)
        {

        }

        public void SaveInventoryScrap(InventoryScrapVM newInventoryScrap)
        {
            try
            {
                Insert(AutoMapper.Mapper.Map<InventoryScrap>(newInventoryScrap));
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
