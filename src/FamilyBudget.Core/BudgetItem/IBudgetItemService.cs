namespace FamilyBudget.Core.Budgets;

public interface IBudgetItemService
{
    Task<PagedResponse<BudgetItemDto>> GetAll(
        BudgetItemFilter filter,
        Pageable pageable,
        Guid currentUser);
}