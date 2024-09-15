using Domain.Abstractions;
using Domain.Abstractions.Commands;
using Domain.Todos;

using static Domain.Todos.TodoItem;
using static Domain.Users.User;

namespace Application.Todos;

public sealed record RemoveTodoListCommand(Guid Id, Guid OwnerId) : ICommand<Result>;

internal sealed class RemoveTodoListCommandHander(ITodoItemRepository taskRepository) : ICommandHandler<RemoveTodoListCommand, Result>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;

    public async Task<Result> HandleAsync(RemoveTodoListCommand command, CancellationToken cancellationToken = default)
    {
        var taskId = new TodoItemId(command.Id);
        var userId = new UserId(command.OwnerId);

        var task = await _taskRepository
            .GetByPredicateAsync(x => x.Id == taskId && x.OwnerId == userId);

        if(task is null)
        {
            return Result.Failure(RemoveTodoListErrors.TodoListNotFoundOrNotOwner);
        }

        _taskRepository.Remove(task);

        return Result.Success();
    }
}

public static class RemoveTodoListErrors
{
    public static Error TodoListNotFoundOrNotOwner = Error.NotFound($"[{nameof(RemoveTodoListCommandHander)}]", "Todo list not found or u didn't owner!");
}

