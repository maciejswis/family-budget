namespace FamilyBudget.Core.Budgets
{
    public interface IExpenseService
    {
        Task<BudgetItemDto?> Add(BudgetItemDto budgetDto, Guid currentUser);
        Task<BudgetItemDto?> Update(BudgetItemDto budgetDto, Guid currentUser);
    }
}