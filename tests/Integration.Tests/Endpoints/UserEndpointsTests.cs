using System.Net.Http.Json;

using Application.Users;

using Integration.Tests.Base;

namespace Integration.Tests.Endpoints;

public class UserEndpointsTests : BaseIntegrationTest
{
    private static string _baseUrl => "api/users";

    public UserEndpointsTests(IntegrationTestWebAppFactory factory)
          : base(factory)
    {
    }

    [Fact]
    public async Task Should_Successfully_CreateUser()
    {
        // Arrange
        var command = new CreateUserCommand("test@ukr.net");

        // Act
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/v1", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
