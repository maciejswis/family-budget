using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using FamilyBudget.Web;
using FamilyBudget.Web.Tests.Fixtures;

namespace FamilyBudget.Web.Tests;

public class LoginControllerTests : IntegrationTest
{
    public LoginControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Get_login_returns_OK()
    {
        var response = await _client.GetAsync("/Login&test=hallo");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_login_returns_string_value()
    {
        var response = await _client.GetAsync("/Login&test=hallo");
        response.Content.Should().Be("teste");
    }
}



