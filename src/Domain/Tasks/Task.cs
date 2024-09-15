using Domain.Abstractions;
using Domain.Users;

using static Domain.Users.User;

namespace Domain.Tasks;

public sealed class Task : BaseEntity
{
    private List<User> _sharedWith = new List<User>();

    private Task() { } // Used by EF Core

    private Task(TaskId id, Name name, UserId ownerId) : base() 
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }

    public readonly record struct TaskId(Guid Id);

    public TaskId Id { get; } = new TaskId(Guid.Empty);

    public Name Name { get; private set; } = null!;

    public Priority Priority { get; private set; } = Priority.Todo;

    public UserId OwnerId { get; }

    public IReadOnlyCollection<User> SharedWith => _sharedWith.AsReadOnly();

    private static Task Init(Name name, UserId ownerId) 
        => new(new TaskId(Guid.NewGuid()), name, ownerId);

    public void Rename(Name name)
    {
        Name = name;
    }

    public void SetPriority(Priority priority)
    {
        Priority = priority;
    }

    public Result ShareWithUser(User user)
    {
        if(_sharedWith.Any(u => u.Id == user.Id))
        {
            return Result.Failure(TaskErrors.UserIsAlreadyShareWith);
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
            return Result.Failure(TaskErrors.UserIsNotFoundForShareWith);
        }

        _sharedWith.Remove(user);

        return Result.Success();
    }
}
