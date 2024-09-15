using System.Linq.Expressions;

using Domain.Abstractions.Queries;
using Domain.Abstractions.Queries.Paging;
using Domain.Todos;

using Infrastructure.Database;

namespace Application.Todos;

public sealed class GetTodoListQuery(string SortColumn, string SortOrder) : PagedQuery<TodoListDto>
{
    public string SortColumn = SortColumn;
    public string SortOrder = SortOrder;
}

internal sealed class GetTodoListQueryHandler(ITodoItemRepository taskRepository) : IQueryHandler<GetTodoListQuery, Paged<TodoListDto>>
{
    private readonly ITodoItemRepository _taskRepository = taskRepository;

    public async Task<Paged<TodoListDto>> HandleAsync(GetTodoListQuery query, CancellationToken cancellationToken = default)
    {
        Expression<Func<TodoItem, object>> keySelector = query.SortColumn switch
        {
            "Name" => task => task.Name,
            "Priority" => task => task.Priority,
            _ => task => task.CreatedAt
        };

        IQueryable<TodoItem> todoListsQuery = _taskRepository
            .GetAll();

        if(query.SortOrder?.ToLower() == "desc")
        {
            todoListsQuery = todoListsQuery.OrderByDescending(keySelector);
        }
        else
        {
            todoListsQuery = todoListsQuery.OrderBy(keySelector);
        }

        var todoLists = todoListsQuery
            .Select(x => new TodoListDto(x.Name.Value, x.Priority));

        return await todoLists.PaginateAsync(query, cancellationToken);
    }
}
