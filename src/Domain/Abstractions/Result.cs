using System.Diagnostics.CodeAnalysis;

namespace Domain.Abstractions;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}

public sealed record Error(string Code, string Description, ErrorType ErrorType)
{
    public static Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error NullValue = new("Error.NullValue", "Null value was provided", ErrorType.Failure);

    public static Error Failure(string code, string description)
        => new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description)
        => new(code, description, ErrorType.Validation);

    public static Error NotFound(string code, string description)
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description)
        => new(code, description, ErrorType.Conflict);

    public override string ToString()
        => $"{Code}, {Description}";
}

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException();

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Errors = [error];
    }

    protected internal Result(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error[] Errors { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result Failure(Error[] errors) => new(false, errors);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static Result<TValue> Failure<TValue>(Error[] errors) => new(default, false, errors);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<T> Ensure<T>(
        T value,
        Func<T, bool> predicate,
        Error error
    )
    {
        return predicate(value) ? Success(value)
                                    : Failure<T>(error);
    }

    public static Result<T> Ensure<T>(
        T value,
        params (Func<T, bool> predicate, Error error)[] functions
    )
    {
        var results = new List<Result<T>>();

        foreach ((Func<T, bool> predicate, Error error) in functions)
            results.Add(Ensure(value, predicate, error));

        return Combine(results.ToArray());
    }

    public static Result<T> Combine<T>(
        params Result<T>[] results
    )
    {
        if (results.Any(r => r.IsFailure))
            return Failure<T>(
                results.SelectMany(r => r.Errors)
                .Distinct()
                .ToArray());

        return Success(results[0].Value);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    protected internal Result(TValue? value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");


    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}

public static class ResultExtensions
{
    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc
    ) => result.IsSuccess ? Result.Success(mappingFunc(result.Value))
                          : Result.Failure<TOut>(result.Errors);
}