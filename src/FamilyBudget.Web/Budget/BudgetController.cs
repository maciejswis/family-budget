using AutoMapper;
using FamilyBudget.Budget;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{ 
    private IBudgetService _service;
    private IUserService _userService;

    public BudgetController(
        IBudgetService service,
        IUserService userService
        )
    { 
        _service = service;
        _userService = userService;
    }

    public static Mapper GetMapper()
    { 
        var config = new MapperConfiguration(cfg =>
        { 
            cfg.CreateMap<BudgetRequest, BudgetDto>();
            cfg.CreateMap<BudgetDto, BudgetResponse>();
        });

        //Create an Instance of Mapper and return that Instance
        var mapper = new Mapper(config);
        return mapper;
    }

    private UserDto GetCurrentUser() => new UserDto { Id = Guid.NewGuid(), Name = "Maciej" };
        //_userService.GetById()


    [HttpPost]
    public BudgetResponse Post(BudgetRequest request)
    {
        var mapper = GetMapper();

        var result = _service.Add(mapper.Map<BudgetDto>(request), GetCurrentUser());

        return mapper.Map<BudgetResponse>(result);
    }

    [HttpGet]
    public async Task<IEnumerable<BudgetResponse>> Get()
    {
        var budgets = await _service.GetAllByUserIdAsync(Guid.NewGuid());
        var mapper = GetMapper();
        return budgets.Select(b => mapper.Map<BudgetResponse>(b));
    }
}
