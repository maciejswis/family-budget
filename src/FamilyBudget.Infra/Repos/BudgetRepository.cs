using System;
using System.Linq.Expressions;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infra
{
    public class BudgetRepository: IRepository<Budget>
    {
        private AppDbContext _context;

        public BudgetRepository(AppDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Budget?> GetByIdAsync(Guid id)
        {
            var budget = await _context.Budgets.Where(u => u.Id == id).SingleOrDefaultAsync();
            return budget;
        }

        public Budget Insert(Budget entity)
        {
            //TODO 
            _context.Database.EnsureCreated();

            return _context.Budgets.Add(entity).Entity;
        } 

        public async Task<ICollection<Budget>> GetAsync(
            Expression<Func<Budget, bool>> filter = null,
            Func<IQueryable<Budget>, IOrderedQueryable<Budget>> orderBy = null,
            string includeProperties = "")
        {
            var result = await _context.Budgets.ToListAsync();
            return result;
        }
    }

    //public class DataRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    //{
    //    internal AppDbContext context; 

    //    public DataRepository(AppDbContext context)
    //    {
    //        this.context = context ?? throw new ArgumentNullException(nameof(context));
    //    }

    //    public virtual IEnumerable<TEntity> Get(
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "")
    //    {
    //        IQueryable<TEntity> query = dbSet;

    //        if (filter != null)
    //        {
    //            query = query.Where(filter);
    //        }

    //        foreach (var includeProperty in includeProperties.Split
    //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //        {
    //            query = query.Include(includeProperty);
    //        }

    //        if (orderBy != null)
    //        {
    //            return orderBy(query).ToList();
    //        }
    //        else
    //        {
    //            return query.ToList();
    //        }
    //    }

    //    public virtual TEntity? GetById(object id)
    //    {
    //        return dbSet.Find(id);
    //    } 

    //    public virtual TEntity Insert(TEntity entity)
    //    {
    //        return context.Budgets Add(entity);
    //    }

    //    public virtual void Delete(object id)
    //    {
    //        TEntity entityToDelete = dbSet.Find(id);
    //        Delete(entityToDelete);
    //    }

    //    public virtual void Delete(TEntity entityToDelete)
    //    {
    //        if (context.Entry(entityToDelete).State == EntityState.Detached)
    //        {
    //            dbSet.Attach(entityToDelete);
    //        }
    //        dbSet.Remove(entityToDelete);
    //    }

    //    public virtual void Update(TEntity entityToUpdate)
    //    {
    //        dbSet.Attach(entityToUpdate);
    //        context.Entry(entityToUpdate).State = EntityState.Modified;
    //    }
    //}
}