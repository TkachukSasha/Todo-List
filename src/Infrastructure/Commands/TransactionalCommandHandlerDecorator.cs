using Domain.Abstractions.Commands;
using Domain.Abstractions;

namespace Infrastructure.Commands;

[Decorator]
internal sealed class TransactionalCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _handler;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalCommandHandlerDecorator(ICommandHandler<TCommand> handler, IUnitOfWork unitOfWork)
    {
        _handler = handler;
        _unitOfWork = unitOfWork;
    }

    public Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        => _unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command, cancellationToken), cancellationToken);
}

[Decorator]
internal sealed class TransactionalCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : class, ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _handler;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalCommandHandlerDecorator(ICommandHandler<TCommand, TResult> handler, IUnitOfWork unitOfWork)
    {
        _handler = handler;
        _unitOfWork = unitOfWork;
    }

    public Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        => _unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command, cancellationToken), cancellationToken);
}