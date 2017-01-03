using System.Linq.Expressions;

namespace System
{
    public static class LambdaGenerator
    {
        public static Expression<Func<TEntity, bool>> ToLambdaForFindByKey<TEntity>(this int id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var property = Expression.Property(parameter, typeof(TEntity).Name + "ID");
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        }
    }
}
