using AutoMapper;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Budgets
{
    public class IncomeService : BudgetItemServiceBase<Income, IncomeDto>, IIncomeService
    {
        public IncomeService(
              IRepository<Income> repository,
              IRepository<Budget> budgetRepository)
         : base(repository, budgetRepository) { }

        protected override Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Income, IncomeDto>();
                cfg.CreateMap<BudgetItemDto, Income>();
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}