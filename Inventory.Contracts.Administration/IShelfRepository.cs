using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IShelfRepository
    {
        List<ShelfVM> GetAllShelves();
        void CreateNewShelf(ShelfVM newShelf);
        void DeleteShelf(int id);
        List<ShelfVM> SearchShelfByName(string ShelfName);
        void UpdateShelf(ShelfVM oldShelf);
    }
}
