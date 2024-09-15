namespace Domain.Users;

public interface IUserRepository
{
    Task<User> GetByPredicateAsync(Func<User, bool> predicate, CancellationToken cancellationToken = default);

    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(User user);
}
