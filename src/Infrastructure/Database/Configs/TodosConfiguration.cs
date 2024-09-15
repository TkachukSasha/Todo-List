using Domain.Todos;
using Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Domain.Todos.TodoItem;

namespace Infrastructure.Database.Configs;

internal sealed class TodosConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .HasConversion(x => x.Value, x => new TodoItemId(x));

        builder
            .Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => Name.Init(x).Value);

        builder
            .HasMany(x => x.SharedWith)
            .WithMany();

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }
}
