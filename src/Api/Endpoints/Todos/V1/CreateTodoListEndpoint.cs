using Application.Todos;

using Infrastructure.Dispatchers;

using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Todos.V1;

public sealed class CreateTodoListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.TodoItems + "v{apiVersion:apiVersion}", async ([FromBody] CreateTodoListCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
