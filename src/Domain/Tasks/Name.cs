using Domain.Abstractions;

namespace Domain.Tasks;

public sealed record Name
{
    private const int MAX_LENGTH = 255;

    private Name(string value) =>
        Value = value;

    public string Value { get; }

    public static Result<Name> Init(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Name>(TaskErrors.NameMustBeProvide);
        }

        if (value.Length > MAX_LENGTH)
        {
            return Result.Failure<Name>(TaskErrors.NameIsOutOfRange);
        }

        return new Name(value);
    }
}
