using Application.Todos;

using Infrastructure.Dispatchers;

using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Todos.V1;

public class RemoveTodoListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.TodoItems + "v{apiVersion:apiVersion}", async ([FromBody] RemoveTodoListCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
