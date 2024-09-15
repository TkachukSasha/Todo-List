using Application.Todos;

using Infrastructure.Dispatchers;

namespace Api.Endpoints.Todos.V1;

public sealed class GetTodoItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.TodoItems + "v{apiVersion:apiVersion}/single", async (Guid id, Guid userId, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetTodoItemQuery(id, userId), cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
