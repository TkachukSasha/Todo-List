using Infrastructure.Dispatchers;
using Application.Todos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Todos.V1
{
    public class RemoveShareTodoItemEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(Routes.TodoItems + "v{apiVersion:apiVersion}/shares", async([FromBody] RemoveShareTodoItemCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
            {
                var result = await dispatcher.SendAsync(command, cancellationToken);

                return Results.Ok(result);
            })
            .WithTags(Tags.TodoItems)
            .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
        }
    }
}
