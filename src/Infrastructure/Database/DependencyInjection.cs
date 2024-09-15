using Domain.Abstractions;
using Domain.Abstractions.Queries.Paging;
using Domain.Abstractions.Queries;
using Domain.Users;

using Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Database.Initializers;
using Domain.Todos;

namespace Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<TodoListContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlActions =>
            {
                sqlActions.CommandTimeout(30);

                sqlActions.EnableRetryOnFailure(1);
            });

            options.EnableSensitiveDataLogging(true);
        });

        services.AddDal<TodoListContext>();

        services
           .AddInitializer<UsersInitializer>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();

        return services;
    }

    public static IServiceCollection AddDal<TContext>(this IServiceCollection services)
       where TContext : DbContext
    {
        services.AddHostedService<DatabaseInitializer<TContext>>();
        services.AddHostedService<DataInitializer>();

        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        return services;
    }

    private static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
      => services.AddTransient<IDataInitializer, T>();

    public static async Task<Paged<TEntity>> PaginateAsync<TEntity>
    (
        this IQueryable<TEntity> data,
        IPagedQuery query,
        CancellationToken cancellationToken = default
    )
    {
        if (query.Page <= 0)
            query.Page = 1;

        switch (query.Results)
        {
            case var value when value <= 0:
                query.Results = 10;
                break;
            case var value when value > 100:
                query.Results = 100;
                break;
            default:
                break;
        }

        var totalResults = await data.CountAsync(cancellationToken);

        var totalPages = totalResults <= query.Results ? 1 : (int)Math.Floor((double)totalResults / query.Results);

        var items = data
             .Skip((query.Page - 1) * query.Results)
             .Take(query.Results);

        var result = await items.ToListAsync(cancellationToken);

        var hasPreviousPage = query.Page > 1;

        var hasNextPage = query.Page * query.Results < totalResults;

        return new Paged<TEntity>
        (
            result,
            query.Page,
            query.Results,
            totalPages,
            totalResults,
            hasPreviousPage,
            hasNextPage
        );
    }
}
