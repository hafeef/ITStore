using Core.Common.Enums;
using System.Data.Entity;

namespace Core.Common.Resolvers
{
    public class StateResolver
    {
        public static EntityState Resolve(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;

                case ObjectState.Modified:
                    return EntityState.Modified;

                case ObjectState.Deleted:
                    return EntityState.Deleted;

                case ObjectState.Detached:
                    return EntityState.Detached;

                case ObjectState.Unchanged:
                    return EntityState.Unchanged;

                default:
                    return EntityState.Added;
            }
        }
    }
}
