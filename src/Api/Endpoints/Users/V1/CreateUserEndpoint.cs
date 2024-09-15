using Application.Users;
using Infrastructure.Dispatchers;

namespace Api.Endpoints.Users.V1;

public sealed class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Users + "v{apiVersion:apiVersion}", async (CreateUserCommand command, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .WithApiVersionSet(VersioningExtensions.GetFirstVersion(app));
    }
}
