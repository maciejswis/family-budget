using FamilyBudget.Core.Entities;

namespace FamilyBudget.Infra;

public class BudgetItemRepository : EfCoreRepository<BudgetItem, AppDbContext>
{
    public BudgetItemRepository(AppDbContext context): base(context) 
    {
    }
}

public class ExpenseRepository: EfCoreRepository<Expense, AppDbContext>
{
    public ExpenseRepository(AppDbContext context): base(context) { }
}

public class IncomeRepository : EfCoreRepository<Income, AppDbContext>
{
    public IncomeRepository(AppDbContext context) : base(context) { }
}
