using Domain.Abstractions;
using Domain.Abstractions.Commands;
using Domain.Todos;
using Domain.Users;

using static Domain.Todos.TodoItem;
using static Domain.Users.User;

namespace Application.Todos;

public sealed record UpdateTodoItemCommand(Guid Id, Guid OwnerId, Priority Priority, string Name) : ICommand<Result>;

internal sealed class UpdateTodoItemCommandHandler(ITodoItemRepository taskRepository, IUserRepository userRepository) : ICommandHandler<UpdateTodoItemCommand, Result>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> HandleAsync(UpdateTodoItemCommand command, CancellationToken cancellationToken = default)
    {
        var taskId = new TodoItemId(command.Id);
        var userId = new UserId(command.OwnerId);

        var task = await _taskRepository.GetByPredicateAsync(x => x.Id == taskId && x.OwnerId == userId, cancellationToken);

        if(task is null) 
        {
            return Result.Failure(UpdateTodoItemErrors.TaskNotFound);
        }

        task.Rename(Name.Init(command.Name).Value);
        task.SetPriority(command.Priority);
        task.ChangeOwner(command.OwnerId);

        return Result.Success(task);
    }
}

public static class UpdateTodoItemErrors
{
    public static Error TaskNotFound = Error.NotFound($"[{nameof(UpdateTodoItemCommandHandler)}]", "Task not found!");
}

