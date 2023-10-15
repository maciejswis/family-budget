using System.Linq.Expressions;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infra
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext context;
        public EfCoreRepository(TContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(object id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity?> Get(object id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<long> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();

        }

        public virtual async Task<ICollection<TEntity>> GetAll(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Pageable pageable = null,
          string includeProperties = "")
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != string.Empty)
            {
                query = query.Include(includeProperties);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageable != null)
            {
               query = query
                    .Skip((pageable.PageNumber - 1) * pageable.PageSize)
                    .Take(pageable.PageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

    } 
}