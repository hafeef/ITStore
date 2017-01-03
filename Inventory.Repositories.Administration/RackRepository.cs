using AutoMapper;
using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class RackRepository : DataRepositoryBase<Rack>, IRackRepository
    {
        public RackRepository(AdminContext context) : base(context)
        {

        }

        public void CreateNewRack(RackVM newRack)
        {
            try
            {
                Insert(Mapper.Map<Rack>(newRack));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteRack(int rackID)
        {
            try
            {
                Delete(rackID);
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
                var racks = FindBy(r => r.IsActive == true);
                return Mapper.Map<List<Rack>, List<RackVM>>(racks);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<RackVM> SearchRackByName(string rackName)
        {
            try
            {
                var racks = FindBy(r => r.IsActive == true && r.Name.Contains(rackName));
                return Mapper.Map<List<Rack>, List<RackVM>>(racks);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateRack(RackVM oldRack)
        {
            try
            {
                var dbRack = FindByKey(oldRack.RackID);
                dbRack = Mapper.Map(oldRack, dbRack);
                Update(dbRack);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
