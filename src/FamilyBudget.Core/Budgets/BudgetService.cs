using System;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Users;
using Microsoft.Extensions.Logging;

namespace FamilyBudget.Core.Budgets
{
    public class BudgetDto
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public Guid UserId { get; set; }
	}

	public interface IBudgetService
	{
		Budget Add(BudgetDto budgetDto, UserDto currentUser);

        ICollection<BudgetDto> GetBudgetItems();

		Task<ICollection<BudgetDto>> GetAllByUserIdAsync(Guid userId);
    }



    public class BudgetService : IBudgetService
	{
        private readonly ILogger<BudgetService> _logger;
		private IRepository<Budget> _repository;

        public BudgetService(
            ILogger<BudgetService> logger,
            IRepository<Budget> repository)
		{
			_logger = logger;
            _repository = repository;
        }

		public ICollection<BudgetDto> GetBudgetItems()
		{
			return new List<BudgetDto>();
		}

		public async Task<ICollection<BudgetDto>> GetAllByUserIdAsync(Guid userId)
		{
			var result = await _repository.GetAsync((b)=> b.Users.Any(u => u.Id == userId));

			return result.Select(res => new BudgetDto
			{
				Id = res.Id,
				Name = res.Name,
				UserId = userId
			}).ToList();
		}


        public Budget Add(BudgetDto budgetDto, UserDto currentUser)
		{
			_logger.LogWarning("Start inserting...");
			var budget = new Budget(budgetDto.Name, new User { Id = currentUser.Id, Name = currentUser.Name } );
			var result = _repository.Insert(budget);
			return result;
		}

		//public Expense CreateExpense(string name, decimal value)
		//{
		//	var expense = new Expense
		//	{
		//		Name = name
		//	};

		//	return Repository.Save(expense);

		//}
	}
}

