using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Budgets
{
    public class BudgetItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public Guid BudgetId { get; set; }

        public virtual BudgetType BudgetType { get; set; }

        public BudgetItemCategory Category { get; set; }

        public Currency Currency { get; set; }
    }

    public class ExpenseDto : BudgetItemDto
    {
        public override BudgetType BudgetType { get { return BudgetType.Expense; } }
    }

    public class IncomeDto : BudgetItemDto
    {
        public override BudgetType BudgetType { get { return BudgetType.Income; } }
    }
}