using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IBrandRepository
    {
        List<BrandVM> GetAllBrands();
        void CreateNewBrand(BrandVM newBrand);
        void DeleteBrand(int id);
        List<BrandVM> SearchBrandByName(string brandName);
        void UpdateBrand(BrandVM brand);
    }
}
