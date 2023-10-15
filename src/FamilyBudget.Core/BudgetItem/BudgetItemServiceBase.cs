using AutoMapper;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Budgets
{
    public abstract class BudgetItemServiceBase<TEntity, TDto>
        where TEntity : BudgetItem
        where TDto : BudgetItemDto
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IRepository<Budget> _budgetRepository;

        public BudgetItemServiceBase(
            IRepository<TEntity> repository,
            IRepository<Budget> budgetRepository)
        {
            _repository = repository;
            _budgetRepository = budgetRepository;
        }

        public async Task<BudgetItemDto?> Add(BudgetItemDto budgetItemDto, Guid currentUser)
        {
            var budget = await GetBudget(budgetItemDto.BudgetId, currentUser);
            if (budget == null)
            {
                return null;
            }

            var mapper = GetMapper();
            var result = await _repository.Add(mapper.Map<TEntity>(budgetItemDto));
            return mapper.Map<TDto>(result);
        }

        public async Task<BudgetItemDto?> Update(BudgetItemDto budgetItemDto, Guid currentUser)
        {
            var budget = await GetBudget(budgetItemDto.BudgetId, currentUser);
            if (budget == null)
            {
                return null;
            }

            var budgetItem = await _repository.Get(budgetItemDto.Id);
            if (budgetItem == null) return null;
            
            budgetItem.Category = budgetItemDto.Category;
            budgetItem.Currency = budgetItemDto.Currency;
            budgetItem.Name = budgetItemDto.Name;
            budgetItem.Value = budgetItemDto.Value;

            var result = await _repository.Update(budgetItem);
            var mapper = GetMapper();
            return mapper.Map<TDto>(result);
        }

        public async Task<bool> Delete(Guid budgetId, Guid budgetItemId, Guid currentUser)
        {
            var budget = await GetBudget(budgetId, currentUser);
            if (budget == null)
            {
                return false;
            }

            var entity = await _repository.Delete(budgetItemId);
            return entity != null;
        }

        private async Task<Budget?> GetBudget(Guid budgetId, Guid currentUser)
        {
            var budgets = await _budgetRepository.GetAll(
                b => b.Id == budgetId && b.Users.Any(u => u.Id == currentUser));
            return budgets.SingleOrDefault();
        }

        protected abstract Mapper GetMapper();
    }
}