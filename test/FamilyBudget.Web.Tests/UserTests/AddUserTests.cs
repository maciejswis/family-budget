using FluentAssertions;
using System.Net;
using FamilyBudget.Web.Tests.Fixtures;
using System.Text;
using FamilyBudget.Web.User;

namespace FamilyBudget.Web.Tests.UserTests;

public class AddUserTests : IntegrationTest
{
    public AddUserTests(ApiWebApplicationFactory fixture) : base(fixture)
    { }

    [Fact]
    public async Task Post_valid_user_should_returns_OK()
    {
        var userData = """
            {
              "fullName": "John Doo",
              "userName": "john",
              "email": "john@host.com"
            }
            """;
        var content = new StringContent(userData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/User", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_user__should_returns_user_data()
    {
        var userData = """
            {
              "fullName": "John Doo",
              "userName": "john",
              "email": "john@host.com"
            }
            """;
        var content = new StringContent(userData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/User", content);
        var respContent = await response.Content.ReadAsStringAsync();
        var userResponse = respContent?.Deserialize<UserResponse>();

        userResponse.Should().NotBeNull();
        userResponse.Id.Should().NotBeEmpty();
        userResponse.FullName.Should().Be("John Doo");
        userResponse.UserName.Should().Be("john");
        userResponse.Email.Should().Be("john@host.com");
    }

    [Fact]
    public async Task Post_invalid_user__should_returns_bad_data()
    {
        var userData = """
            {
              "fullName": "John Doo",
              "userName": "",
              "email": "john@host.com"
            }
            """;
        var content = new StringContent(userData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/User", content);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}


