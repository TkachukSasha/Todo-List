using System.Linq.Expressions;

namespace Domain.Tasks;

public interface ITaskRepository
{
    Task<Task?> GetByPredicateAsync(Expression<Func<Task, bool>> predicate, CancellationToken cancellationToken = default);
    Task<List<Task>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<dynamic>> GetAllAsync(Expression<Func<Task, bool>> predicate, Expression<Func<Task, dynamic>> selector, CancellationToken cancellationToken = default);

    void Add(Task task);
    void Remove(Task entity);
}
