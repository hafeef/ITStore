using Inventory.Contracts.Inventory;
using Inventory.DomainClasses.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data.Inventory;
using Inventory.ViewModels.Inventory;
using System.Transactions;
using Core.Common.Enums;
using Core.Common.Resolvers;
using AutoMapper.QueryableExtensions;

namespace Inventory.Repositories.Inventory
{
    public class TransferRepository : DataRepositoryBase<Transfer>, ITransferRepository
    {
        public TransferRepository(InventoryContext context) : base(context)
        {

        }

        public void DeleteTransfers(TransferVM transfer)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    var dbTransfers = AutoMapper.Mapper.Map<Transfer>(transfer);
                    context.Entry(dbTransfers).State = StateResolver.Resolve(dbTransfers.EntityState);
                    context.SaveChanges();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void SaveTransfers(IEnumerable<TransferVM> transfers)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (InventoryContext context = new InventoryContext())
                    {
                        var dbTransfers = AutoMapper.Mapper.Map<IEnumerable<TransferVM>, IEnumerable<Transfer>>(transfers);
                        foreach (var item in dbTransfers.Where(li => li.EntityState != ObjectState.Unchanged))
                            context.Entry(item).State = StateResolver.Resolve(item.EntityState);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<TransferVM> SearchTransfers(int itemID)
        {
            return SearchTransfers(itemID, null);
        }

        public List<TransferVM> SearchTransfers(int itemID, string[] serialNos)
        {
            using (InventoryContext context = new InventoryContext())
            {
                using (AdminReferenceContext adminContext = new AdminReferenceContext())
                {
                    IQueryable<Transfer> query;
                    if (serialNos != null)
                        query = context.Transfers.AsNoTracking()
                                               .Where(t => t.ItemID == itemID && serialNos.Contains(t.SerialNo) && t.IsActive == true);
                    else
                        query = context.Transfers.AsNoTracking()
                                               .Where(t => t.ItemID == itemID && t.IsActive == true);

                    var dbTransfers = query.ToList();
                    var transfers = AutoMapper.Mapper.Map<IList<Transfer>, IList<TransferVM>>(dbTransfers);

                    int[] warehouseIDs = dbTransfers.Select(t => t.FromWarehouseID)
                                                    .Union(dbTransfers.Select(t => t.ToWarehouseID))
                                                    .ToArray();
                    int[] rackIDs = dbTransfers.Select(t => t.FromRackID)
                                               .Union(dbTransfers.Select(t => t.ToRackID))
                                               .ToArray();
                    int[] shelfIDs = dbTransfers.Select(t => t.FromShelfID)
                                                .Union(dbTransfers.Select(t => t.ToShelfID))
                                                .ToArray();

                    var items = adminContext.Items.AsNoTracking().Where(i => i.ItemID == itemID).ToList();
                    var warehouses = adminContext.Warehouses.AsNoTracking().Where(wh => warehouseIDs.Contains(wh.WareHouseID)).ToList();
                    var racks = adminContext.Racks.AsNoTracking().Where(r => rackIDs.Contains(r.RackID)).ToList();
                    var shelves = adminContext.Shelves.AsNoTracking().Where(s => shelfIDs.Contains(s.ShelfID)).ToList();

                    transfers.Join(items, tvm => tvm.ItemID, i => i.ItemID, (tvm, i) => { tvm.ItemDescription = i.Description; return tvm; })
                                           .ToList();

                    transfers.Join(warehouses, tvm => tvm.FromWarehouseID, w => w.WareHouseID, (tvm, w) => { tvm.FromWarehouseName = w.Name; return tvm; })
                                           .ToList();
                    transfers.Join(warehouses, tvm => tvm.ToWarehouseID, w => w.WareHouseID, (tvm, w) => { tvm.ToWarehouseName = w.Name; return tvm; })
                                           .ToList();

                    transfers.Join(racks, tvm => tvm.FromRackID, r => r.RackID, (tvm, r) => { tvm.FromRackName = r.Name; return tvm; })
                                           .ToList();
                    transfers.Join(racks, tvm => tvm.ToRackID, r => r.RackID, (tvm, r) => { tvm.ToRackName = r.Name; return tvm; })
                                           .ToList();

                    transfers.Join(shelves, tvm => tvm.FromShelfID, s => s.ShelfID, (tvm, s) => { tvm.FromShelfName = s.Name; return tvm; })
                                           .ToList();
                    return transfers.Join(shelves, tvm => tvm.ToShelfID, s => s.ShelfID, (tvm, s) => { tvm.ToShelfName = s.Name; return tvm; }).OrderBy(tvm => tvm.SerialNo).ThenByDescending(tvm => tvm.TransferDate)
                                            .ToList();
                }
            }
        }
    }
}
