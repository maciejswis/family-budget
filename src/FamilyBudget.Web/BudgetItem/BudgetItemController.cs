using AutoMapper;
using FamilyBudget.Core;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Users;
using FamilyBudget.Web.Budget;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyBudget.Web.BudgetItem;


[ApiController]
[Route("/budget/{budgetId}/[controller]")]
public class BudgetItemController : FamilyBudgetControllerBase
{
    private readonly IBudgetItemService _service;

    public BudgetItemController(IBudgetItemService service, IUserService userService)
        : base(userService)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<PagedResponse<BudgetItemResponse>> Get(
        Guid budgetId,
        Guid currentUser,
       [FromQuery] BudgetItemFilter filter,
       [FromQuery] Pageable pageable
        )
    {
        var user = await GetCurrentUser(currentUser);
        filter.BudgetId = budgetId;
        var validPagable = new Pageable(pageable.PageSize, pageable.PageNumber);
        var pagedResult = await _service.GetAll(filter, validPagable, user.Id);
        var mapper = GetMapper();
        return new PagedResponse<BudgetItemResponse>
        {
            Results = pagedResult.Results.Select(mapper.Map<BudgetItemResponse>).ToList(),
            Total = pagedResult.Total,
            PageSize = validPagable.PageSize,
            CurrentPage = validPagable.PageNumber
        }; 
    } 

    public static Mapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BudgetItemDto, BudgetItemResponse>();
        });

        //Create an Instance of Mapper and return that Instance
        var mapper = new Mapper(config);
        return mapper;
    }
}

