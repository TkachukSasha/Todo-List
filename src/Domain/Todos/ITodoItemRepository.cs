using System.Linq.Expressions;

namespace Domain.Todos;

public interface ITodoItemRepository
{
    Task<TodoItem?> GetByPredicateAsync(Expression<Func<TodoItem, bool>> predicate, CancellationToken cancellationToken = default);
    Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<dynamic>> GetAllAsync(Expression<Func<TodoItem, bool>> predicate, Expression<Func<TodoItem, dynamic>> selector, CancellationToken cancellationToken = default);

    IQueryable<TodoItem> GetAll();

    void Add(TodoItem task);
    void AddRange(IEnumerable<TodoItem> tasks);
    void Remove(TodoItem entity);
    void RemoveRange(IEnumerable<TodoItem> tasks);
}
