using Infrastructure.Dispatchers;

using Microsoft.Extensions.DependencyInjection;

namespace Integration.Tests.Base;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IServiceScope _serviceScope;

    protected readonly IDispatcher _dispatcher;

    protected readonly HttpClient? _httpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();

        _dispatcher = _serviceScope.ServiceProvider.GetRequiredService<IDispatcher>();

        _httpClient = factory.Client;
    }
}