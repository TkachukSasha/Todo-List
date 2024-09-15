using Domain.Abstractions;
using Domain.Users;

using static Domain.Users.User;

namespace Domain.Todos;

public sealed class TodoItem : BaseEntity
{
    private List<User> _sharedWith = new List<User>();

    private TodoItem() { } // Used by EF Core

    private TodoItem(TodoItemId id, Name name, UserId ownerId) : base() 
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }

    public readonly record struct TodoItemId(Guid Value)
    {
        public static implicit operator Guid(TodoItemId taskId) => taskId.Value;
        public static implicit operator TodoItemId(Guid id) => new TodoItemId(id);
    }

    public TodoItemId Id { get; } = new TodoItemId(Guid.Empty);

    public Name Name { get; private set; } = null!;

    public Priority Priority { get; private set; } = Priority.Todo;

    public UserId OwnerId { get; private set; }

    public IReadOnlyCollection<User> SharedWith => _sharedWith.AsReadOnly();

    public static TodoItem Init(Name name, UserId ownerId) 
        => new(new TodoItemId(Guid.NewGuid()), name, ownerId);

    public void Rename(Name name)
    {
        Name = name;
    }

    public void SetPriority(Priority priority)
    {
        Priority = priority;
    }

    public void ChangeOwner(UserId ownerId)
    {
        OwnerId = ownerId;
    }

    public Result ShareWithUser(User user)
    {
        if(_sharedWith.Any(u => u.Id == user.Id))
        {
            return Result.Failure(error: TodoItemErrors.UserIsAlreadyShareWith);
        }

        _sharedWith.Add(user);

        return Result.Success();
    }

    public Result RemoveShareWithUser(UserId ownerId)
    {
        var user = _sharedWith
            .FirstOrDefault(u => u.Id == ownerId);

        if(user is null)
        {
            return Result.Failure(TodoItemErrors.UserIsNotFoundForShareWith);
        }

        _sharedWith.Remove(user);

        return Result.Success();
    }
}
