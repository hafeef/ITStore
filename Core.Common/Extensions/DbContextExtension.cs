using Core.Common.Contracts;
using Core.Common.Resolvers;
using System.Data.Entity;

namespace Core.Common.Extensions
{
    public static class DbContextExtension
    {
        public static void FixStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
            {
                IObjectState entity = entry.Entity;
                entry.State = StateResolver.Resolve(entity.EntityState);
            }
        }
    }
}
