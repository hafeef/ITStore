using System;
using System.Collections.Generic;
using Inventory.Contracts.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.Data.Administration;
using AutoMapper;
using Inventory.ViewModels.Administration;

namespace Inventory.Repositories.Administration
{
    public class BrandRepository : DataRepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(AdminContext context) : base(context)
        {

        }

        public void CreateNewBrand(BrandVM newBrand)
        {
            try
            {
                Insert(Mapper.Map<Brand>(newBrand));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteBrand(int brandID)
        {
            try
            {
                Delete(brandID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



        public List<BrandVM> GetAllBrands()
        {
            try
            {
                var brands = FindBy(brand => brand.IsActive == true);
                return Mapper.Map<List<Brand>, List<BrandVM>>(brands);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<BrandVM> SearchBrandByName(string brandName)
        {
            try
            {
                var brands = FindBy(brand => brand.IsActive == true && brand.Name.Contains(brandName));
                return Mapper.Map<List<Brand>, List<BrandVM>>(brands);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateBrand(BrandVM brand)
        {
            try
            {
                var dbBrand = FindByKey(brand.BrandID);
                dbBrand = Mapper.Map(brand, dbBrand);
                Update(dbBrand);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
