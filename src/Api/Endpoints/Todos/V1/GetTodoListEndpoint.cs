using Application.Todos;

using Infrastructure.Dispatchers;

namespace Api.Endpoints.Todos.V1;

public sealed class GetTodoListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.TodoItems + "v{apiVersion:apiVersion}", async(string sortColumn, string sortOrder, int page, int results, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetTodoListQuery(sortColumn, sortOrder)
            {
                Page = page,
                Results = results
            }, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.TodoItems)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
