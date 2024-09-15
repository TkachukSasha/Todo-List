using Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Domain.Users.User;

namespace Infrastructure.Database.Configs;

internal sealed class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .HasConversion(x => x.Value, x => new UserId(x));

        builder
            .Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.Property(x => x.Email)
             .HasConversion(x => x.Value, x => Email.Init(x).Value);

        builder
            .HasIndex(x => x.Email, "idx_users_email");
    }
}
