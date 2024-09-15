using System.Linq.Expressions;

namespace Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);

    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);

    IQueryable<User> GetAll();

    void Add(User user);
}
