using System.Linq.Expressions;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infra
{
    public class BudgetRepository : EfCoreRepository<Budget, AppDbContext>
    {
        public BudgetRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<ICollection<Budget>> GetAll(
             Expression<Func<Budget, bool>> filter = null,
             Func<IQueryable<Budget>, IOrderedQueryable<Budget>> orderBy = null,
             Pageable pageable = null,
             string includeProperties = "")
        {
            IQueryable<Budget> query = context.Budgets;
            query.Include(b => b.Users);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != string.Empty)
            {
                query = query.Include(includeProperties);
            }

            if (pageable != null)
            {
                query
                    .Skip((pageable.PageNumber - 1) * pageable.PageSize)
                    .Take(pageable.PageSize);
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

    }
}