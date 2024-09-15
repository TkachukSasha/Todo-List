using Domain.Abstractions.Commands;
using Domain.Abstractions.Queries;

namespace Infrastructure.Dispatchers;

public interface IDispatcher
{
    Task HandleAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}