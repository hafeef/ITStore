using Core.Common.BaseTypes;
using Inventory.Data.Inventory;

namespace Inventory.Repositories.Inventory
{
    public abstract partial class DataRepositoryBase<TEntity> : DataRepository<TEntity, InventoryContext>
         where TEntity : EntityBase
    {
        public DataRepositoryBase(InventoryContext context) : base(context)
        {

        }
    }
}
