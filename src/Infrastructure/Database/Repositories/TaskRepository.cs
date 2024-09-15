using Domain.Tasks;

namespace Infrastructure.Database.Repositories;

internal sealed class TaskRepository : Repository<Domain.Tasks.Task>, ITaskRepository
{
    public TaskRepository(TodoListContext todoListContext) : base(todoListContext)
    {
    }
}
