using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Contracts.Inventory
{
    public interface IInventoryScrapRepository
    {
        void SaveInventoryScrap(InventoryScrapVM newInventoryScrap);
        HashSet<InventoryScrapVM> GetAllInventoryScraps();
    }
}
