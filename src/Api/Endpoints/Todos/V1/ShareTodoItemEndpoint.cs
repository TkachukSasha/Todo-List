using Infrastructure.Dispatchers;
using Application.Todos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Todos.V1;

public sealed class ShareTodoItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.TodoItems + "v{apiVersion:apiVersion}/shares", async ([FromBody] ShareTodoItemCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
