using System.Net;
using FamilyBudget.Core.Exceptions;
using FamilyBudget.Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Web;

public class FamilyBudgetControllerBase : ControllerBase
{
    protected IUserService _userService;
    public FamilyBudgetControllerBase(IUserService userService)
    {
        _userService = userService;
    }

    protected async Task<UserDto> GetCurrentUser(Guid userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null)
        {
            throw new HttpResponseException(
                (int)HttpStatusCode.NotFound,
                "User not found.");
        }

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
