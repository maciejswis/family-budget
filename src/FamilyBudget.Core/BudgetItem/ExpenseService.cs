using AutoMapper;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Budgets
{

    public class ExpenseService : BudgetItemServiceBase<Expense, ExpenseDto>, IExpenseService
    { 
        public ExpenseService(
            IRepository<Expense> repository,
            IRepository<Budget> budgetRepository)
       : base(repository, budgetRepository) { }

         
        protected override Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Expense, ExpenseDto>();
                cfg.CreateMap<BudgetItemDto, Expense>();
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}