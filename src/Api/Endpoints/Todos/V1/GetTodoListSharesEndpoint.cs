using Application.Todos;

using Infrastructure.Dispatchers;

namespace Api.Endpoints.Todos.V1;

public sealed class GetTodoListSharesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.TodoItems + "v{apiVersion:apiVersion}/shares", async (Guid taskId, Guid ownerId, Guid userId, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetTodoListSharesQuery(taskId, ownerId, userId), cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
