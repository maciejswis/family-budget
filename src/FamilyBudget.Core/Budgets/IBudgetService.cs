namespace FamilyBudget.Core.Budgets
{
    public interface IBudgetService
    {
        Task<BudgetDto> Add(BudgetDto budgetDto, Guid currentUser);

        ICollection<BudgetDto> GetBudgetItems();

        Task<PagedResponse<BudgetDto>> GetAllByUserId(Guid userId, Pageable? pageable = null);

        Task<bool> Share(Guid budgetId, Guid userId, Guid currentUser);

        Task<BudgetDto> Update(BudgetDto budgetDto, Guid currentUser);
    }
}

