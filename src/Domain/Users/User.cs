using Domain.Abstractions;

namespace Domain.Users;

public sealed class User : BaseEntity
{
    public readonly record struct UserId(Guid Id);

    private User() { } // Used by EF Core

    private User(UserId id, Email email) : base() 
    { 
        Id = id;
        Email = email;
    }

    public UserId Id { get; } = new UserId(Guid.Empty);

    public Email Email { get; } = null!;

    public static User Init(Email email) =>
        new User(new UserId(Guid.NewGuid()), email);
}
