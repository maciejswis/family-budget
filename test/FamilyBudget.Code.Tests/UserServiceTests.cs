using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Users;
using FluentAssertions;
using Moq;

namespace FamilyBudget.Code.Tests;

public class BudgetServiceTests
{

}

public class UserServiceTests
{ 
    [Fact]
    public async Task AddUser()
    {
        var repoMock = new Mock<IRepository<User>>();
        repoMock.Setup(r => r.Add(It.IsAny<User>())).ReturnsAsync((User u) =>
        {
            u.Id = Guid.NewGuid();
            return u;
        });

        var service = new UserService(repoMock.Object);

        var actual = await service.Add(GetUserDto());

        actual.Id.Should().NotBeEmpty();
        actual.UserName.Should().Be("tom");
    }

    private static UserDto GetUserDto()
    {
        return new UserDto
        {
            Email = "tom@host.com",
            UserName = "tom",
            FullName = "Tomas"
        };
    }
}
