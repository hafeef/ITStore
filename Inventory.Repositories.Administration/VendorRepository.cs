using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class VendorRepository : DataRepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(AdminContext context) : base(context)
        {
        }

        public void CreateNewVendor(VendorVM newVendor)
        {
            try
            {
                Insert(AutoMapper.Mapper.Map<Vendor>(newVendor));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteVendor(int vendorID)
        {
            try
            {
                Delete(vendorID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<VendorVM> GetAllVendors()
        {
            try
            {
                var vendors = FindBy(v => v.IsActive == true);
                return AutoMapper.Mapper.Map<List<Vendor>, List<VendorVM>>(vendors);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<VendorVM> SearchVendorByName(string vendorName)
        {
            try
            {
                var vendors = FindBy(v => v.IsActive == true && v.Name.Contains(vendorName));
                return AutoMapper.Mapper.Map<List<Vendor>, List<VendorVM>>(vendors);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateVendor(VendorVM vender)
        {
            try
            {
                var dbVendor = FindByKey(vender.VendorID);
                dbVendor = Mapper.Map(vender, dbVendor);
                Update(dbVendor);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
