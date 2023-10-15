using FluentAssertions;
using System.Net;
using FamilyBudget.Core;
using FamilyBudget.Web.Tests.Fixtures;
using FamilyBudget.Web.User;

namespace FamilyBudget.Web.Tests.UserTests;

public class GetAllUsersTests : IntegrationTest
{
    public GetAllUsersTests(ApiWebApplicationFactory fixture) : base(fixture)
    { }

    [Fact]
    public async Task Get_users_returns_OK()
    {
        var response = await _client.GetAsync("/User");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_users_returns_proper_page_info()
    {
        var response = await _client.GetAsync("/User?PageNumber=2&PageSize=6&SortBy=UserName&SortDirection=ASC");
        var content = await response.Content.ReadAsStringAsync();
        var pagedResponse = content?.Deserialize<PagedResponse<UserResponse>>();
 
        pagedResponse.Should().NotBeNull();
        pagedResponse.Total.Should().Be(2);
        pagedResponse.PageSize.Should().Be(6);
        pagedResponse.CurrentPage.Should().Be(2);
        pagedResponse.TotalPages.Should().Be(1);
        pagedResponse.HasPrevious.Should().BeTrue();
        pagedResponse.HasNext.Should().BeFalse();
    }

    [Fact]
    public async Task Get_users_returns_proper_page_data()
    {
        var response = await _client.GetAsync("/User?PageNumber=1&PageSize=10");
        var content = await response.Content.ReadAsStringAsync();
        var pagedResponse = content?.Deserialize<PagedResponse<UserResponse>>();

        pagedResponse.Should().NotBeNull();
        pagedResponse.Results.Should().HaveCount(2);
    }
}


