using Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Common.BaseTypes
{
    public abstract class DataRepository<TEntity, TContext> : IDisposable
      where TEntity : EntityBase
      where TContext : DbContext, new()
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;

        public DataRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected List<TEntity> All()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbSet.AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected TEntity FindByKey(int id)
        {
            try
            {
                return _dbSet.AsNoTracking().FirstOrDefault(id.ToLambdaForFindByKey<TEntity>());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected List<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                return GetAllIncluding(includeProperties).Where(predicate).ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected List<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                return GetAllIncluding(includeProperties).ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        protected TEntity Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected int BulkInsert(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                return _context.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected TEntity Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateGraph(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                ApplyStateChanges();
                _context.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected int Delete(int id)
        {
            try
            {
                var entity = FindByKey(id);
                entity.IsActive = false;
                _context.Entry(entity).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ApplyStateChanges()
        {
            _context.FixStateChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DataRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
