using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Web.Budget;

public class BudgetRequest
{
    [Required]
    public string Name { get; set; }

    // TODO replace with Keycloak (or other) token based user id
    public Guid CurrentUser { get; set; }
}

public class BudgetResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}