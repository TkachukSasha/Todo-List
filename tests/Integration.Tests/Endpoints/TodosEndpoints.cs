using Application.Users;
using System.Net.Http.Json;

using Integration.Tests.Base;
using Application.Todos;

namespace Integration.Tests.Endpoints;

public class TodosEndpoints : BaseIntegrationTest
{
    private static string _baseUrl => "api/todos";

    public TodosEndpoints(IntegrationTestWebAppFactory factory)
          : base(factory)
    {
    }

    [Fact]
    public async Task Should_Successfully_CreateTodoList()
    {
        // Arrange
        var todo = new TodoListDto("test", Domain.Todos.Priority.Todo);

        var command = new CreateTodoListCommand(Guid.NewGuid(), [todo]);

        // Act
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/v1", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Successfully_ShareTodoList()
    {
        // Arrange
        var command = new ShareTodoItemCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        // Act
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/v1/shares", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Successfully_UpdateTodoItem()
    {
        // Arrange
        var command = new UpdateTodoItemCommand(Guid.NewGuid(), Guid.NewGuid(), Domain.Todos.Priority.Done, "changed");

        // Act
        var response = await _httpClient!.PostAsJsonAsync($"{_baseUrl}/v1", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
