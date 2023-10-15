namespace FamilyBudget.Core.Budgets
{
    public interface IIncomeService
    {
        Task<BudgetItemDto?> Add(BudgetItemDto budgetDto, Guid currentUser);
        Task<BudgetItemDto?> Update(BudgetItemDto budgetDto, Guid currentUser);
        Task<bool> Delete(Guid budgetId, Guid budgetItemId, Guid currentUser);
    }
}