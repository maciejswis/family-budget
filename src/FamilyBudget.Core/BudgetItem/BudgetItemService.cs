using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Exceptions;

namespace FamilyBudget.Core.Budgets
{
    public class BudgetItemFilter
    {
        public BudgetItemCategory? Category { get; set; }
        public string? Name { get; set; }
        public Guid BudgetId { get; set; }
    }

    public class BudgetItemService : IBudgetItemService
    {
        private readonly IRepository<BudgetItem> _repository;
        private readonly IRepository<Budget> _budgetRepository;

        public BudgetItemService(IRepository<BudgetItem> repository, IRepository<Budget> budgetRepository)
        {
            _repository = repository;
            _budgetRepository = budgetRepository;
        }

        private async Task<bool> CanAccess(Guid budgetId, Guid userId)
        {
            var budgets = await _budgetRepository.GetAll(
              b => b.Id == budgetId && b.Users.Any(u => u.Id == userId));
            return budgets.SingleOrDefault() != null;
        }

        public async Task<PagedResponse<BudgetItemDto>> GetAll(
            BudgetItemFilter filter,
            Pageable pageable,
            Guid currentUser)
        {

            var access = await CanAccess(filter.BudgetId, currentUser);
            if (!access)
            {
                throw new HttpResponseException(
                    (int)HttpStatusCode.NotFound,
                    "Budget not found. Either does not exist, or You don't have permission to it.");
            }

            Expression<Func<BudgetItem, bool>> filterExp = (b) =>
                b.BudgetId == filter.BudgetId
                && (filter.Category == null || b.Category == filter.Category)
                && (string.IsNullOrEmpty(filter.Name) || b.Name.StartsWith(filter.Name));

            var results = await _repository.GetAll(
                filterExp,
                pageable.GetOrderSpec<BudgetItem>(bi => bi.Name),
                pageable);
            var total = await _repository.Count(filterExp);

            var mapper = GetMapper();
            return new PagedResponse<BudgetItemDto>
            {
                Results = results.Select(mapper.Map<BudgetItemDto>).ToList(),
                Total = total,
            };
        }

        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BudgetItem, BudgetItemDto>()
                .BeforeMap((src, dest) =>
                {
                    dest.BudgetType = src is Expense ? BudgetType.Expense : BudgetType.Income;
                });
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}