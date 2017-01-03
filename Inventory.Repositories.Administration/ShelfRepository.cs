using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class ShelfRepository : DataRepositoryBase<Shelf>, IShelfRepository
    {
        public ShelfRepository(AdminContext context) : base(context)
        {
        }

        public void CreateNewShelf(ShelfVM newShelf)
        {
            try
            {
                Insert(Mapper.Map<Shelf>(newShelf));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteShelf(int shelfID)
        {
            try
            {
                Delete(shelfID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ShelfVM> GetAllShelves()
        {
            try
            {
                var shelves = FindBy(s => s.IsActive == true);
                return Mapper.Map<List<Shelf>, List<ShelfVM>>(shelves);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ShelfVM> SearchShelfByName(string shelfName)
        {
            try
            {
                var shelves = FindBy(s => s.IsActive == true && s.Name.Contains(shelfName));
                return Mapper.Map<List<Shelf>, List<ShelfVM>>(shelves);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateShelf(ShelfVM oldShelf)
        {
            try
            {
                var dbShelf = FindByKey(oldShelf.ShelfID);
                dbShelf = Mapper.Map(oldShelf, dbShelf);
                Update(dbShelf);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
