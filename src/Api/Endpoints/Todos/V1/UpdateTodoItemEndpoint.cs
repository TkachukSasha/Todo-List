using Application.Todos;

using Infrastructure.Dispatchers;

using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Todos.V1;

public sealed class UpdateTodoItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.TodoItems + "v{apiVersion:apiVersion}", async ([FromBody] UpdateTodoItemCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
