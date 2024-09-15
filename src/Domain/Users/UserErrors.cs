using Domain.Abstractions;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailMustBeProvide = Error.Validation($"[{nameof(User)}]", "Email must be provide!");

    public static readonly Error EmailIsInvalid = Error.Validation($"[{nameof(User)}]", "Email is invalid!");

    public static readonly Error EmailIsOutOfRange = Error.Validation($"[{nameof(User)}]", "Email is out of range!");
}
