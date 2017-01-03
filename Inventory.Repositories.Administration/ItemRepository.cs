using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class ItemRepository : DataRepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(AdminContext context) : base(context)
        {
        }

        public void CreateNewItem(ItemVM newItem)
        {
            try
            {
                Insert(Mapper.Map<Item>(newItem));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteItem(int itemID)
        {
            try
            {
                Delete(itemID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemVM> GetAllItems()
        {
            try
            {
                var items = FindByInclude(i => i.IsActive == true, i => i.ItemType, i => i.Brand, i => i.Category);
                return Mapper.Map<List<Item>, List<ItemVM>>(items);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemVM> SearchItemByDescription(string itemDescription)
        {
            try
            {
                var items = FindByInclude(i => i.IsActive == true && i.Description.Contains(itemDescription), i => i.ItemType, i => i.Brand, i => i.Category);
                return Mapper.Map<List<Item>, List<ItemVM>>(items);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateItem(ItemVM item)
        {
            try
            {
                var dbItem = FindByKey(item.ItemID);
                dbItem = Mapper.Map(item, dbItem);
                Update(dbItem);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
