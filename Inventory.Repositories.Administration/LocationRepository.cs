using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class LocationRepository : DataRepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(AdminContext context) : base(context)
        {

        }

        public void CreateNewLocation(LocationVM newLocation)
        {
            try
            {
                var location = Insert(Mapper.Map<Location>(newLocation));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteLocation(int locationID)
        {
            try
            {
                var location = Delete(locationID);
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
                var locations = FindBy(location => location.IsActive == true);
                return Mapper.Map<List<Location>, List<LocationVM>>(locations);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<LocationVM> SearchLocationByName(string locationName)
        {
            try
            {
                var locations = FindBy(location => location.IsActive == true && location.Name.Contains(locationName));
                return Mapper.Map<List<Location>, List<LocationVM>>(locations);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateLocation(LocationVM location)
        {
            try
            {
                var dbLocation = FindByKey(location.LocationID);
                dbLocation = Mapper.Map(location, dbLocation);
                Update(dbLocation);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
