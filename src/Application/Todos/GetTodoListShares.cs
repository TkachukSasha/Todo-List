using Application.Users;

using Domain.Abstractions.Queries;
using Domain.Todos;

using Microsoft.EntityFrameworkCore;

using static Domain.Todos.TodoItem;
using static Domain.Users.User;

namespace Application.Todos;

public sealed record GetTodoListSharesQuery(Guid TaskId, Guid OwnerId, Guid UserId) : IQuery<List<UserDto>>;

internal sealed class GetTodoListSharesQueryHandler(ITodoItemRepository taskRepository) : IQueryHandler<GetTodoListSharesQuery, List<UserDto>>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;

    public async Task<List<UserDto>> HandleAsync(GetTodoListSharesQuery query, CancellationToken cancellationToken = default)
    {
        var taskId = new TodoItemId(query.TaskId);
        var userId = new UserId(query.UserId);

        IQueryable<TodoItem> todoList = _taskRepository
            .GetAll();

        return await todoList
            .Include(x => x.SharedWith)
            .Where(x => x.Id == taskId
                && (x.OwnerId == userId || x.SharedWith.Any(x => x.Id == userId)))
            .SelectMany(x => x.SharedWith.Select(u => new UserDto(u.Id.Value, u.Email.Value)))
            .ToListAsync(cancellationToken);
    }
}
