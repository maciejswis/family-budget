using FamilyBudget.Core;

namespace FamilyBudget.Web.User;

public class UserPageableRequest : Pageable
{
    public string? UserName { get; set; }
}
