using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface ILocationRepository
    {
        List<LocationVM> GetAllLocations();
        void CreateNewLocation(LocationVM location);
        void DeleteLocation(int locationID);
        List<LocationVM> SearchLocationByName(string locationName);
        void UpdateLocation(LocationVM location);
    }
}
