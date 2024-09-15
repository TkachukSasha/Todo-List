using Domain.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

internal sealed class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    public UnitOfWork(TContext dbContext) 
        => _dbContext = dbContext;

    public Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
