using Domain.Users;

namespace Infrastructure.Database.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TodoListContext todoListContext) : base(todoListContext)
    {
    }
}
