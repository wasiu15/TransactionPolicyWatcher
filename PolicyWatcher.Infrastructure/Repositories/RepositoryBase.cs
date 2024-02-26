using Microsoft.EntityFrameworkCore;
using PolicyWatcher.Domain.Interfaces.Repository;
using PolicyWatcher.Infrastructure.Data;
using System.Linq.Expressions;

namespace PolicyWatcher.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PolicyWatcherDbContext RepositoryContext;
        public RepositoryBase(PolicyWatcherDbContext repositoryContext) => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>()
            .AsNoTracking() : RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<T>().Where(expression)
            .AsNoTracking() : RepositoryContext.Set<T>().Where(expression);

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void UpdateBulk(List<T> entities) => RepositoryContext.Set<T>().UpdateRange(entities);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
