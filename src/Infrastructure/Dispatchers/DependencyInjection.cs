using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dispatchers;

public static class DependencyInjection
{
    public static IServiceCollection AddDispatchers(this IServiceCollection services)
        => services.AddSingleton<IDispatcher, Dispatcher>();
}