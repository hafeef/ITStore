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

namespace Inventory.Repositories.Inventory
{
    public class TransferRepository : DataRepositoryBase<Transfer>, ITransferRepository
    {
        public TransferRepository(InventoryContext context) : base(context)
        {
        }

        public void DeleteTransfers(IEnumerable<TransferVM> transfers)
        {
            throw new NotImplementedException();
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

        public IList<TransferVM> SearchTransfers(int itemID)
        {
            throw new NotImplementedException();
        }

        public IList<TransferVM> SearchTransfers(int itemID, string[] serialNos)
        {
            throw new NotImplementedException();
        }
    }
}
