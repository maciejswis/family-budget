using AutoMapper;
using FamilyBudget.Core;
using FamilyBudget.Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Web.User;


[ApiController]
[Route("[controller]")]
public class UserController: FamilyBudgetControllerBase
{
    public UserController(IUserService userService) : base(userService) { }

    [HttpPost]
    public async Task<UserResponse> Post(UserRequest request)
    {
        var mapper = GetMapper();
        var user = mapper.Map<UserDto>(request);
        var result = await _userService.Add(user);

        return mapper.Map<UserResponse>(result);
    }

    [HttpGet]
    public async Task<PagedResponse<UserResponse>> Get([FromQuery] UserPageableRequest pageable)
    {
        var validPagable = new Pageable(pageable.PageSize, pageable.PageNumber);
        var pagedResult = await _userService.GetAll(pageable.UserName, validPagable);
        var mapper = GetMapper();
        return new PagedResponse<UserResponse> {
            Results = pagedResult.Results.Select(mapper.Map<UserResponse>).ToList(),
            Total = pagedResult.Total,
            PageSize = validPagable.PageSize,
            CurrentPage = validPagable.PageNumber,
        };
    }

    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserRequest, UserDto>();
            cfg.CreateMap<UserDto, UserResponse>();
        });

        //Create an Instance of Mapper and return that Instance
        var mapper = new Mapper(config);
        return mapper;
    }
}
