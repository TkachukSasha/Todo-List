using Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

internal sealed class TasksConfiguration : IEntityTypeConfiguration<Domain.Tasks.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Tasks.Task> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasMany(x => x.SharedWith)
            .WithMany();

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }
}
