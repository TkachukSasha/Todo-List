using Application.Users;
using Domain.Abstractions.Queries.Paging;

using Infrastructure.Dispatchers;

namespace Api.Endpoints.Users.V1;

public sealed class GetUsersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Users + "v{apiVersion:apiVersion}", async (int page, int results, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            Paged<UserDto> result = await dispatcher.QueryAsync(new GetUsersQuery
            {
                Page = page,
                Results = results
            }, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
