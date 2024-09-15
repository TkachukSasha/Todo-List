using System.Reflection;

using Domain.Abstractions;
using Domain.Abstractions.Commands;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Commands;

public static class DependencyInjection
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assemblies = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.Scan(s => s.FromAssemblies(assemblies)
           .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
               .WithoutAttribute<DecoratorAttribute>())
           .AsImplementedInterfaces()
           .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        return services;
    }

    public static IServiceCollection AddTransactionalCommandDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        services.TryDecorate(typeof(ICommandHandler<,>), typeof(TransactionalCommandHandlerDecorator<,>));

        return services;
    }
}