using Domain.Abstractions;

namespace Domain.Todos;

public static class TodoItemErrors
{
    public static readonly Error NameMustBeProvide = Error.Validation($"[{nameof(Task)}]", "Name must be provide!");

    public static readonly Error NameIsOutOfRange = Error.Validation($"[{nameof(Task)}]", "Name is out of range!");

    public static readonly Error UserIsNotFoundForShareWith = Error.Validation($"[{nameof(Task)}]", "User is not found for share with!");

    public static readonly Error UserIsAlreadyShareWith = Error.Validation($"[{nameof(Task)}]", "User is already share with!");
}
