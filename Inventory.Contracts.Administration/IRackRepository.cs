using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IRackRepository
    {
        List<RackVM> GetAllRacks();
        void CreateNewRack(RackVM newRack);
        void DeleteRack(int id);
        List<RackVM> SearchRackByName(string rackName);
        void UpdateRack(RackVM oldRack);
    }
}
