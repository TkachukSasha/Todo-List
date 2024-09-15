using System.Reflection;

using Domain.Abstractions;
using Domain.Abstractions.Queries;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Queries;

public static class DependencyInjection
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var assemblies = Assembly.GetCallingAssembly();

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddPagedQueryDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(IQueryHandler<,>), typeof(PagedQueryHandlerDecorator<,>));

        return services;
    }
}