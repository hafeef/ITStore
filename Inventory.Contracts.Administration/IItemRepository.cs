using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IItemRepository
    {
        List<ItemVM> GetAllItems();
        void CreateNewItem(ItemVM newItem);
        void DeleteItem(int itemID);
        List<ItemVM> SearchItemByDescription(string itemDescription);
        void UpdateItem(ItemVM item);
    }
}
