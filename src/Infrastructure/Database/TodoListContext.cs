using Domain.Todos;
using Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class TodoListContext : DbContext
{
    public TodoListContext() { }

    public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<TodoItem> Todos => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoListContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
