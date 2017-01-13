using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

namespace Inventory.Contracts.Inventory
{
    public interface IPurchaseOrderRepository
    {
        void CreatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        PurchaseOrderVM FindPurchaseOrderByPoOrContractNumber(string poOrContractNumber);
        PurchaseOrderVM FindPurchaseOrderIncludeReceivedItemsByPoOrContractNumber(string poOrContractNumber);
        void UpdatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        List<VendorVM> GetAllVendors();
        List<ItemVM> GetItemByDescription(string itemDescription);
        List<LocationVM> GetAllLocations();
        List<WareHouseVM> GetAllWarehouses();
        List<RackVM> GetAllRacks();
        List<ShelfVM> GetAllShelves();
        bool IsPurchaseOrderExists(string poOrContractNumber);
    }
}
