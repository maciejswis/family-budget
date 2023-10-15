using FamilyBudget.Core.Entities;
using Microsoft.Extensions.Logging;

namespace FamilyBudget.Core.Budgets
{
    public class BudgetService : IBudgetService
    {
        private readonly ILogger<BudgetService> _logger;
        private readonly IRepository<Budget> _repository;
        private readonly IRepository<User> _userRepository;

        public BudgetService(
            ILogger<BudgetService> logger,
            IRepository<Budget> repository,
            IRepository<User> userRepository)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
        }

        public ICollection<BudgetDto> GetBudgetItems()
        {
            return new List<BudgetDto>();
        }

        public async Task<PagedResponse<BudgetDto>> GetAllByUserId(
            Guid userId, Pageable? pageable = null)
        {
            var result = await _repository.GetAll(
                b => b.Users.Any(u => u.Id == userId),
                pageable?.GetOrderSpec<Budget>(b => b.Name),
                pageable
                );

            var total = await _repository.Count(b => b.Users.Any(u => u.Id == userId));
            return
                new PagedResponse<BudgetDto>
                {
                    Results = result.Select(MapToDto).ToList(),
                    Total = total
                };
        }

        public async Task<BudgetDto> Add(BudgetDto budgetDto, Guid currentUser)
        {
            var user = await _userRepository.Get(currentUser);
            var budget = new Budget(budgetDto.Name, user);
            var result = await _repository.Add(budget);
            return MapToDto(result);
        }

        private async Task<Budget?> GetBudget(Guid budgetId, Guid currentUser)
        {
            var budgets = await _repository.GetAll(
              b => b.Id == budgetId && b.Users.Any(u => u.Id == currentUser));
            return budgets.SingleOrDefault();
        }

        public async Task<BudgetDto> Update(BudgetDto budgetDto, Guid currentUser)
        {
            var budget = await GetBudget(budgetDto.Id, currentUser);
            if (budget == null)
            {
                return null;
            }

            budget.Name = budgetDto.Name;
            var result = await _repository.Update(budget);
            return MapToDto(result);
        }

        private static BudgetDto MapToDto(Budget budget) =>
            new BudgetDto
            {
                Name = budget.Name,
                Id = budget.Id
            };


        public async Task<bool> Share(Guid budgetId, Guid userId, Guid currentUser)
        {
            var budget = await GetBudget(budgetId, currentUser);
            var user = await _userRepository.Get(userId);
            if (budget == null || user == null)
            {
                return false;
            }

            budget.Users.Add(user);
            await _repository.Update(budget);

            return true;
        }
    }
}

