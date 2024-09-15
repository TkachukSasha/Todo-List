using Domain.Abstractions.Queries;
using Domain.Todos;

using static Domain.Todos.TodoItem;
using static Domain.Users.User;

namespace Application.Todos;

public sealed record TaskItemDto(Guid Id, Guid OwnerId, string Name, Priority Priority);

public sealed record GetTodoItemQuery(Guid Id, Guid UserId) : IQuery<TodoItem>;

internal sealed class GetTodoItemQueryHandler(ITodoItemRepository taskRepository) : IQueryHandler<GetTodoItemQuery, TodoItem>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;

    public async Task<TodoItem> HandleAsync(GetTodoItemQuery query, CancellationToken cancellationToken = default)
    {
        var taskId = new TodoItemId(query.Id);
        var userId = new UserId(query.UserId);

        return await _taskRepository
            .GetByPredicateAsync(x => x.Id == taskId
                && (x.OwnerId == userId || x.SharedWith.Any(x => x.Id == userId)), cancellationToken);
    }
}
