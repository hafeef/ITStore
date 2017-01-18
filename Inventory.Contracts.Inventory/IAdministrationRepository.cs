using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Contracts.Inventory
{
    public interface IAdministrationRepository
    {
        List<VendorVM> GetAllVendors();
        List<ItemVM> GetItemByDescription(string itemDescription);
        List<LocationVM> GetAllLocations();
        List<WareHouseVM> GetAllWarehouses();
        List<RackVM> GetAllRacks();
        List<ShelfVM> GetAllShelves();
    }
}
