using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Web.User;

public class UserRequest
{
    public string FullName { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
}
