using System.Linq.Expressions;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infra
{
    public static class SqlExceptionNumber
    {
        public const int DUPLICATE_INSERT = 2601;
    }

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
            try { 
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                SqlException innerException = null;
                Exception tmp = e;
                while (innerException == null && tmp != null)
                {
                    if (tmp != null)
                    {
                        innerException = tmp.InnerException as SqlException;
                        tmp = tmp.InnerException;
                    }

                }
                if (innerException != null
                    && innerException.Number == SqlExceptionNumber.DUPLICATE_INSERT)
                {
                    throw new Core.Exceptions.NotUniqueException(
                        $"Could not add {entity.GetType().Name} - value is not unique");
                }
                else
                {
                    throw new DataAccessException(
                        "Could not add {entity.GetType().Name}, Try again later. If problem persist contact system administrator.",
                        e
                        );
                }
            }
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