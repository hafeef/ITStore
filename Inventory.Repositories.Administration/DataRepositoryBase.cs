using Core.Common.BaseTypes;
using Inventory.Data.Administration;

namespace Inventory.Repositories.Administration
{
    public abstract partial class DataRepositoryBase<TEntity> : DataRepository<TEntity, AdminContext>
         where TEntity : EntityBase
    {
        public DataRepositoryBase(AdminContext context) : base(context)
        {

        }
    }
}
