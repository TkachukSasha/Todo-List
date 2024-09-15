using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Infrastructure.Database;
using Infrastructure.Commands;
using Infrastructure.Queries;
using Infrastructure.Dispatchers;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommands();

        services.AddTransactionalCommandDecorator();

        services.AddQueries();

        services.AddPagedQueryDecorator();

        services.AddDispatchers();

        services.AddDataLayer(configuration.GetConnectionString("Default")!);

        return services;
    }
}
