using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IWarehouseRepository
    {
        List<WareHouseVM> GetAllWarehouses();
        void CreateNewWarehouse(WareHouseVM newWarehouse);
        void DeleteWarehouse(int wareHouseId);
        List<WareHouseVM> SearchWarehouseByName(string warehouseName);
        void UpdateWarehouse(WareHouseVM warehouse);
    }
}
