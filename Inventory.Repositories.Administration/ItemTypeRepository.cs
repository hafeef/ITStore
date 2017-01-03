using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class ItemTypeRepository : DataRepositoryBase<ItemType>, IItemTypeRepository
    {
        public ItemTypeRepository(AdminContext context) : base(context)
        {
        }

        public void CreateNewItemType(ItemTypeVM newItemType)
        {
            try
            {
                var itemType = Insert(Mapper.Map<ItemType>(newItemType));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteItemType(int itemTypeID)
        {
            try
            {
                var itemType = Delete(itemTypeID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemTypeVM> GetAllItemTypes()
        {
            try
            {
                var itemTypes = FindBy(itemType => itemType.IsActive == true);
                return Mapper.Map<List<ItemType>, List<ItemTypeVM>>(itemTypes);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemTypeVM> SearchItemTypeByName(string itemTypeName)
        {
            try
            {
                var itemTypes = FindBy(itemType => itemType.IsActive == true && itemType.Name.Contains(itemTypeName));
                return Mapper.Map<List<ItemType>, List<ItemTypeVM>>(itemTypes);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateItemType(ItemTypeVM itemType)
        {
            try
            {
                var dbItemType = FindByKey(itemType.ItemTypeID);
                dbItemType = Mapper.Map(itemType, dbItemType);
                Update(dbItemType);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
