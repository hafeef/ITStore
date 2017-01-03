using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IItemTypeRepository
    {
        List<ItemTypeVM> GetAllItemTypes();
        void CreateNewItemType(ItemTypeVM newItemType);
        void DeleteItemType(int id);
        List<ItemTypeVM> SearchItemTypeByName(string itemTypeName);
        void UpdateItemType(ItemTypeVM itemType);
    }
}
