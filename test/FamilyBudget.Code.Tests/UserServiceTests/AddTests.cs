using FamilyBudget.Core;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Users;
using FluentAssertions;
using Moq;

namespace FamilyBudget.Code.Tests.UserServiceTests;

public class AddTests
{
    private readonly Mock<IRepository<User>> userRepoMock = new();

    public AddTests()
    {
        SetupUserRepoMock();
    }

    private void SetupUserRepoMock()
    {
        userRepoMock.Setup(r => r.Add(It.IsAny<User>()))
            .ReturnsAsync((User u) =>
            {
                u.Id = Guid.NewGuid();
                return u;
            });
    }

    [Fact]
    public async Task Add__Should_return_user_dto()
    {
        var dto = GetUserDto();
        var sut = new UserService(userRepoMock.Object);

        var actual = await sut.Add(dto);

        actual.Id.Should().NotBeEmpty();
        actual.UserName.Should().Be(dto.UserName);
        actual.FullName.Should().Be(dto.FullName);
        actual.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task Add__Should_store_user()
    {
        var dto = GetUserDto();
        var sut = new UserService(userRepoMock.Object);

        var actual = await sut.Add(dto);

        userRepoMock.Verify(repo => repo.Add(
            It.Is<User>(u => u.UserName == dto.UserName)
        ));
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
