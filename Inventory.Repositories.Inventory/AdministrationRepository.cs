using Inventory.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.ViewModels.Inventory;
using Inventory.Data.Inventory;
using Inventory.DomainClasses.Inventory;

namespace Inventory.Repositories.Inventory
{
    public class AdministrationRepository : IAdministrationRepository
    {
        public List<VendorVM> GetAllVendors()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var vendors = context.Vendors.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Vendor>, List<VendorVM>>(vendors);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemVM> GetItemByDescription(string itemDescription)
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var items = context.Items.AsNoTracking().Where(i => i.Description.Contains(itemDescription)).ToList();
                    return AutoMapper.Mapper.Map<List<Item>, List<ItemVM>>(items);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<LocationVM> GetAllLocations()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var locations = context.Locations.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Location>, List<LocationVM>>(locations);
                }
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
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var warehouses = context.Warehouses.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Warehouse>, List<WareHouseVM>>(warehouses);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<RackVM> GetAllRacks()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var racks = context.Racks.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Rack>, List<RackVM>>(racks);
                }
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
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var shelves = context.Shelves.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Shelf>, List<ShelfVM>>(shelves);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<EmployeeVM> GetAllEmployee()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var shelves = context.Employees.AsNoTracking().ToList();
                    return AutoMapper.Mapper.Map<List<Employee>, List<EmployeeVM>>(shelves);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<EmployeeVM> FindEmployeeByCivilID(string civilID)
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var shelves = context.Employees.AsNoTracking().Where(e => e.CivilID.StartsWith(civilID)).ToList();
                    return AutoMapper.Mapper.Map<List<Employee>, List<EmployeeVM>>(shelves);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
