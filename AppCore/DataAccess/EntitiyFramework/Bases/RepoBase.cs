using System.Linq.Expressions;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;

namespace AppCore.DataAccess.EntitiyFramework.Bases
{
    public abstract class RepoBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        protected readonly DbContext _dbContext;

        protected RepoBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[]entitiesToInclude) // Read -> C(R)UD
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var entityToInclude in entitiesToInclude)
            {
                query = query.Include(entityToInclude);
            }
            return query;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] entitiesToInclude)
        {
            return Query(entitiesToInclude).Where(predicate);
        }

        public void Dispose() // işlem sonucu null ise çöpe atılır
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
