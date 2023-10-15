using System.Linq.Expressions;
using FamilyBudget.Code.Tests.Builders;
using FamilyBudget.Core;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Entities;
using FluentAssertions;
using Moq;

namespace FamilyBudget.Code.Tests.BudgetServiceTests;

public class ShareTests
{
    private Guid budgetId;
    private Guid actAs;
    private Guid userId;
    private Mock<IRepository<User>> userRepoMock;
    private Mock<IRepository<Budget>> budgetRepoMock;

    public ShareTests()
    {
        budgetId = Guid.NewGuid();
        actAs = Guid.NewGuid();
        userId = Guid.NewGuid();

        userRepoMock = new Mock<IRepository<User>>();
        budgetRepoMock = new Mock<IRepository<Budget>>();
        SetupUserRepoMock();
    }

    private void SetupUserRepoMock()
    {
        userRepoMock.Setup(u => u.Get(userId))
            .ReturnsAsync(new UserBuilder().WithId(userId).Build());
    }

    private void SetupBudgetRepoMock(ICollection<Budget> results)
    {
        budgetRepoMock.Setup(u => u.GetAll(
           It.IsAny<Expression<Func<Budget, bool>>>(),
           It.IsAny<Func<IQueryable<Budget>, IOrderedQueryable<Budget>>>(),
           It.IsAny<Pageable>(),
           It.IsAny<string>()
       ))
           .ReturnsAsync(results);
    }

    [Fact]
    public async Task Share_By_user_without_access_to_budget__should_return_false()
    {
        // Budget not found for user
        SetupBudgetRepoMock(results: Array.Empty<Budget>());

        var sut = new BudgetService(budgetRepoMock.Object, userRepoMock.Object);

        var userWithoutAccessToBudget = Guid.NewGuid();

        var result = await sut.Share(budgetId, userId, userWithoutAccessToBudget);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Share_By_user_with_access_to_budget__should_return_true()
    {
        var budget = new BudgetBuilder().WithId(budgetId).Build();
        SetupBudgetRepoMock(results: new Budget[] { budget });

        var sut = new BudgetService(budgetRepoMock.Object, userRepoMock.Object);

        var result = await sut.Share(budgetId, userId, actAs);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Share_By_user_with_access_to_budget__should_update_budget()
    {
        var budget = new BudgetBuilder().WithId(budgetId).Build();
        SetupBudgetRepoMock(results: new Budget[] { budget });

        var sut = new BudgetService(budgetRepoMock.Object, userRepoMock.Object);

        await sut.Share(budgetId, userId, actAs);

        budgetRepoMock.Verify(repo => repo.Update(
            It.Is<Budget>(b => b.Users.Any())));
    }
}
