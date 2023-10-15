using System.Linq.Expressions;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Budgets
{
    public static class Sort
    {
        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, SortDirection? direction = SortDirection.ASC)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == SortDirection.ASC ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(orderBy);
        }


        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderSpec<TEntity>(this Pageable pageable, Expression<Func<TEntity, object>> defaultValue)
        {
            if (string.IsNullOrEmpty(pageable.SortBy))
            {
                return q => pageable.SortDirection == SortDirection.DESC ? q.OrderByDescending(defaultValue) : q.OrderBy(defaultValue);
            }

            return q => q.OrderByDynamic(pageable.SortBy, pageable.SortDirection);
        }
    }
}