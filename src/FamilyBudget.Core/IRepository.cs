using System.Linq.Expressions;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
	{
		public TEntity Insert(TEntity entity);

		public Task<TEntity?> GetByIdAsync(Guid id);

        public Task<ICollection<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}

