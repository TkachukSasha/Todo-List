using Domain.Abstractions;
using Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Initializers;

internal sealed class UsersInitializer(TodoListContext context, ILogger<UsersInitializer> logger) : IDataInitializer
{
    private readonly TodoListContext _context = context;
    private readonly ILogger<UsersInitializer> _logger = logger;

    public async Task InitAsync()
    {
        if (await _context.Users.AnyAsync()) return;

        await InitUsersAsync();
    }

    private async Task InitUsersAsync()
    {
        var user = User.Init(Email.Init("sashatkachuk09@ukr.net").Value);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }
}
