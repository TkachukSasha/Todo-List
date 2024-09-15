using System.Text.RegularExpressions;

using Domain.Abstractions;

namespace Domain.Users;

public sealed record Email
{
    private const int MAX_LENGTH = 50;

    private static readonly Regex _regex = new Regex(
               @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
               RegexOptions.Compiled);

    private Email(string value) =>
        Value = value;

    public string Value { get; }

    public static Result<Email> Init(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(UserErrors.EmailMustBeProvide);
        }

        if (value.Length > MAX_LENGTH)
        {
            return Result.Failure<Email>(UserErrors.EmailIsOutOfRange);
        }

        if (!_regex.IsMatch(value))
        {
            return Result.Failure<Email>(UserErrors.EmailIsInvalid);
        }

        return new Email(value);
    }
}
