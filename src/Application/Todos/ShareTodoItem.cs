using Domain.Abstractions;
using Domain.Abstractions.Commands;
using Domain.Todos;
using Domain.Users;

using static Domain.Todos.TodoItem;
using static Domain.Users.User;

namespace Application.Todos;

public sealed record ShareTodoItemCommand(Guid TaskId, Guid OwnerId, Guid UserId) : ICommand<Result>;

internal sealed class ShareTodoItemCommandHandler(ITodoItemRepository taskRepository, IUserRepository userRepository) : ICommandHandler<ShareTodoItemCommand, Result>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> HandleAsync(ShareTodoItemCommand command, CancellationToken cancellationToken = default)
    {
        var taskId = new TodoItemId(command.TaskId);
        var userId = new UserId(command.OwnerId);

        var user = await _userRepository.GetByPredicateAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(CreateTodoListErrors.OwnerNotFound);
        }

        var task = await _taskRepository
            .GetByPredicateAsync(x => x.Id == taskId && x.OwnerId == userId, cancellationToken);

        if(task is null)
        {
            return Result.Failure(ShareTodoItemErrors.TodoListNotFoundOrNotOwner);
        }

        task.ShareWithUser(user);

        return Result.Success();
    }
}

public static class ShareTodoItemErrors
{
    public static Error UserNotFound = Error.NotFound($"[{nameof(ShareTodoItemCommandHandler)}]", "User not found!");
    public static Error TodoListNotFoundOrNotOwner = Error.NotFound($"[{nameof(ShareTodoItemCommandHandler)}]", "Todo list not found or u didn't owner!");
}

