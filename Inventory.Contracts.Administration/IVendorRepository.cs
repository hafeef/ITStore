using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IVendorRepository
    {
        List<VendorVM> GetAllVendors();
        void CreateNewVendor(VendorVM newVendor);
        void DeleteVendor(int vendorID);
        List<VendorVM> SearchVendorByName(string vendorName);
        void UpdateVendor(VendorVM vender);
    }
}
