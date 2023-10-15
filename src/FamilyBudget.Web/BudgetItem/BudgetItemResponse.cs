using FamilyBudget.Core;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Web.BudgetItem;

public class BudgetItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public BudgetType BudgetType { get; set; }
    public decimal Value { get; set; }
    public Currency Currency { get; set; }
    public BudgetItemCategory Category { get; set; }
}

public class BudgetItemRequest
{
    public string Name { get; set; }
    public string Value { get; set; }
    public Currency Currency { get; set; }
    public BudgetItemCategory Category { get; set; } 
}

public class BudgetItemPagedRequest : Pageable
{
    public BudgetType? BudgetType { get; set; }
    public BudgetItemCategory? Category { get; set; }
    public string? Name { get; set; }
}
 