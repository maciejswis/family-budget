using AutoMapper;
using FamilyBudget.Core;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Users;
using FamilyBudget.Web.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyBudget.Web.Budget;

[ApiController]
[Route("[controller]")]
public class BudgetController : FamilyBudgetControllerBase
{
    private IBudgetService _service;

    public BudgetController(
        IBudgetService service,
        IUserService userService) : base(userService)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<BudgetResponse> Post(BudgetRequest request)
    {
        var user = await GetCurrentUser(request.CurrentUser);
        var mapper = GetMapper();

        var result = await _service.Add(mapper.Map<BudgetDto>(request),user.Id);

        return mapper.Map<BudgetResponse>(result);
    }

    [HttpPut]
    [Route("id")]
    public async Task<BudgetResponse> Put(Guid id, BudgetRequest request)
    {
        var user = await GetCurrentUser(request.CurrentUser);
        var mapper = GetMapper();
        var dto = mapper.Map<BudgetDto>(request);
        dto.Id = id;

        var result = await _service.Update(dto, user.Id);

        return mapper.Map<BudgetResponse>(result);
    }

    [HttpPost]
    [Route("{budgetId}/share-with/{userId}")]
    public async Task<bool> Share(Guid budgetId, Guid userId, Guid currentUser)
    {
        var user = await GetCurrentUser(currentUser);
        var result = await _service.Share(budgetId, userId, user.Id);
        return result;
    }

    [HttpGet]
    public async Task<PagedResponse<BudgetResponse>> Get(
        [FromQuery] Pageable pageable,
        Guid currentUser)
    {
        var validPagable = new Pageable(pageable.PageSize, pageable.PageNumber);
        var pagedResult = await _service.GetAllByUserId(currentUser, validPagable);
        var mapper = GetMapper();
        return new PagedResponse<BudgetResponse>
        {
            Results = pagedResult.Results.Select(mapper.Map<BudgetResponse>).ToList(),
            Total = pagedResult.Total,
            PageSize = validPagable.PageSize,
            CurrentPage = validPagable.PageNumber,
        };
    }

    private static Mapper GetMapper()
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

}
