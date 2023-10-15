using System.Net;
using AutoMapper;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Exceptions;
using FamilyBudget.Core.Users;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Web.BudgetItem;

[ApiController]
[Route("/budget/{budgetId}/[controller]")]
public class ExpenseController : FamilyBudgetControllerBase
{
    private IExpenseService _service;

    public ExpenseController(
        IExpenseService expenseSerivce,
        IUserService userService) : base(userService)
    {
        _service = expenseSerivce;
    }

    [HttpPost]
    public async Task<BudgetItemResponse> Post(Guid budgetId, BudgetItemRequest request, Guid currentUser)
    {
        var user = await GetCurrentUser(currentUser);
        var mapper = GetMapper();
        var item = mapper.Map<BudgetItemDto>(request);
        item.BudgetId = budgetId;
        var result = await _service.Add(item, user.Id);
        if (result == null)
        {
            throw new HttpResponseException((int)HttpStatusCode.NotFound, "Budget not found. Either does not exist, or You don't have permission to it.");
        }

        return mapper.Map<BudgetItemResponse>(result);
    }


    [HttpPut]
    [Route("{expenseId}")]
    public async Task<BudgetItemResponse> Put(Guid budgetId, Guid expenseId, BudgetItemRequest request, Guid currentUser)
    {
        var user = await GetCurrentUser(currentUser);
        var mapper = GetMapper();
        var item = mapper.Map<BudgetItemDto>(request);
        item.BudgetId = budgetId;
        item.Id = expenseId;

        var result = await _service.Update(item, user.Id);
        if (result == null)
        {
            throw new HttpResponseException((int)HttpStatusCode.NotFound, "Budget not found. Either does not exist, or You don't have permission to it.");
        }

        return mapper.Map<BudgetItemResponse>(result);
    }

    [HttpDelete]
    [Route("{expenseId}")]
    public async Task<bool> Delete(Guid budgetId, Guid expenseId, Guid currentUser)
    {
        return await _service.Delete(budgetId, expenseId, currentUser);
    }

    public static Mapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BudgetItemRequest, BudgetItemDto>()
            .BeforeMap((src, dest) => dest.BudgetType = Core.Entities.BudgetType.Expense);
            cfg.CreateMap<BudgetItemDto, BudgetItemResponse>();
        });

        //Create an Instance of Mapper and return that Instance
        var mapper = new Mapper(config);
        return mapper;
    }
}

