using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Web.Auth;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet]
    public async Task<string> GetLogin(string test)
    {
        return test + "!!";
    }
}