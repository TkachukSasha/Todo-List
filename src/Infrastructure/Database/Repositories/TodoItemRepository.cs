using Domain.Todos;

namespace Infrastructure.Database.Repositories;

internal sealed class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(TodoListContext todoListContext) : base(todoListContext)
    {
    }
}
