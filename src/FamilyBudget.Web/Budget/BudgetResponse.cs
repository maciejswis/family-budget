using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Budget;

public class BudgetItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    // public currency, value, budgetType
    public decimal Value { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}

public class ExpenseRequest
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class BudgetRequest
{
    [Required]
    public string Name { get; set; }

    // TODO replace with Keycloak (or other) token based user id
    public Guid CurrentUser { get; set; }
}

public class BudgetResponse
{
    public string Name { get; set; }

    //public List<UserResponse> SharedWith { get; set; }
}