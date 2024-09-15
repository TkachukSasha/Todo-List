using Domain.Abstractions;
using Domain.Abstractions.Commands;
using Domain.Todos;
using Domain.Users;

namespace Application.Todos;
public sealed record TodoListDto(string Name, Priority Priority);

public sealed record CreateTodoListCommand(Guid OwnerId, List<TodoListDto> TodoLists) : ICommand<Result>;

internal sealed class CreateTodoListCommandHandler(ITodoItemRepository taskRepository, IUserRepository userRepository) : ICommandHandler<CreateTodoListCommand, Result>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> HandleAsync(CreateTodoListCommand command, CancellationToken cancellationToken = default)
    {
        var userId = new User.UserId(command.OwnerId);

        var user = await _userRepository.GetByPredicateAsync(x => x.Id == userId, cancellationToken);

        if(user is null)
        {
            return Result.Failure(CreateTodoListErrors.OwnerNotFound);
        }

        List<TodoItem> tasks = command
            .TodoLists
            .AsParallel()
            .Select(x => TodoItem.Init(Name.Init(x.Name).Value, command.OwnerId))
            .ToList();

        _taskRepository.AddRange(tasks);

        return Result.Success();
    }
}

public static class CreateTodoListErrors
{
    public static Error OwnerNotFound = Error.NotFound($"[{nameof(CreateTodoListCommandHandler)}]", "Owner not found!");
}
