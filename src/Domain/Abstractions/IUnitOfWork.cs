namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);

    Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default);
}