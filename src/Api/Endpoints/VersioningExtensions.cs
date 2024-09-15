using Asp.Versioning;
using Asp.Versioning.Builder;

namespace Api.Endpoints;

public static class VersioningExtensions
{
    public static ApiVersionSet GetFirstVersion(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
           .HasApiVersion(new ApiVersion(1))
           .ReportApiVersions()
           .Build();

        return apiVersionSet;
    }
}
