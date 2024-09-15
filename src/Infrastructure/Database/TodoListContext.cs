using Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class TodoListContext : DbContext
{
    public TodoListContext() { }

    public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Domain.Tasks.Task> Tasks => Set<Domain.Tasks.Task>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoListContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
