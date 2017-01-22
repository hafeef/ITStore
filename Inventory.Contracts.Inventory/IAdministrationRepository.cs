using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

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
        List<EmployeeVM> GetAllEmployee();
        List<EmployeeVM> FindEmployeeByCivilID(string civilID);
    }
}
