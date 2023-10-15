using System.Linq.Expressions;
using FamilyBudget.Code.Tests.Builders;
using FamilyBudget.Core;
using FamilyBudget.Core.Budgets;
using FamilyBudget.Core.Entities;
using FluentAssertions;
using Moq;

namespace FamilyBudget.Code.Tests.BudgetServiceTests;

public class AddTests
{
    private Guid actAs;
    private Mock<IRepository<User>> userRepoMock;
    private Mock<IRepository<Budget>> budgetRepoMock;

    public AddTests()
    {
        actAs = Guid.NewGuid();
        userRepoMock = new Mock<IRepository<User>>();
        budgetRepoMock = new Mock<IRepository<Budget>>();
        SetupUserRepoMock();
        SetupBudgetRepoMock();
    }

    private void SetupUserRepoMock()
    {
        userRepoMock.Setup(u => u.Get(actAs))
            .ReturnsAsync(new UserBuilder().WithId(actAs).Build());
    }

    private void SetupBudgetRepoMock()
    {
        budgetRepoMock.Setup(u => u.Add(It.IsAny<Budget>()))
            .ReturnsAsync((Budget b) =>
            {
                b.Id = Guid.NewGuid();
                return b;
            });
    }

    [Fact]
    public async Task Add__should_return_dto()
    {
        var dto = new BudgetDto { Name = "new budget" };

        var sut = new BudgetService(budgetRepoMock.Object, userRepoMock.Object);

        var result = await sut.Add(dto, actAs);

        result.Name.Should().Be(dto.Name);
        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Add_By_user__should_assign_user_to_bugdet()
    {
        var dto = new BudgetDto { Name = "new budget" };

        var sut = new BudgetService(budgetRepoMock.Object, userRepoMock.Object);

        await sut.Add(dto, actAs);

        budgetRepoMock.Verify(repo => repo.Add(
            It.Is<Budget>(b =>
                b.Name == dto.Name && b.Users.Any(u => u.Id == actAs))
            ));
    }
}
