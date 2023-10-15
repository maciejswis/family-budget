using System;
using System.Linq;
using System.Linq.Expressions;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infra
{

    public class UserRepository : EfCoreRepository<User, AppDbContext>
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        //public async Task<User?> GetByIdAsync(Guid id)
        //{
        //    var user = await _context.Users.Where(u => u.Id == id).SingleOrDefaultAsync();
        //    return user;
        //}

        //public async Task<User> InsertAsync(User user)
        //{
        //    var result = _context.Users.Add(user).Entity;
        //    await _context.SaveChangesAsync();
        //    return result;
        //}

        //public async virtual Task<ICollection<User>> GetAllAsync(
        //    Expression<Func<User, bool>> filter = null,
        //    Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    IQueryable<User> users = _context.Users;

        //    if (filter != null)
        //    {
        //        users = users.Where(filter);
        //    }

        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        users = users.Include(includeProperty);
        //    }

        //    if (orderBy != null)
        //    {
        //        return orderBy(users).ToList();
        //    }
        //    else
        //    {
        //        return await users.ToListAsync();
        //    }
        //} 
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