using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class WarehouseRepository : DataRepositoryBase<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(AdminContext context) : base(context)
        {

        }

        public void CreateNewWarehouse(WareHouseVM newWarehouse)
        {
            try
            {
                var warehouse = Mapper.Map<Warehouse>(newWarehouse);
                Insert(warehouse);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteWarehouse(int wareHouseID)
        {
            try
            {
                var warehouse = Delete(wareHouseID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<WareHouseVM> GetAllWarehouses()
        {
            try
            {
                var warehouses = FindByInclude(wh => wh.IsActive == true, wh => wh.Location);
                return AutoMapper.Mapper.Map<List<Warehouse>, List<WareHouseVM>>(warehouses);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<WareHouseVM> SearchWarehouseByName(string warehouseName)
        {
            try
            {
                var warehouses = FindByInclude(wh => wh.IsActive == true && wh.Name.Contains(warehouseName), wh => wh.Location);
                return AutoMapper.Mapper.Map<List<Warehouse>, List<WareHouseVM>>(warehouses);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateWarehouse(WareHouseVM warehouse)
        {
            try
            {
                var dbWareHouse = FindByKey(warehouse.WareHouseID);
                dbWareHouse = Mapper.Map(warehouse, dbWareHouse);
                Update(dbWareHouse);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
