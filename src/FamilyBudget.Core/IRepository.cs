using System.Linq.Expressions;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> Get(object id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(object id);

        Task<ICollection<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Pageable pageable = null,
            string includeProperties = "");

        Task<long> Count(Expression<Func<TEntity, bool>> filter = null);
    }
}

