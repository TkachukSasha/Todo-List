namespace Domain.Abstractions;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public DateTime CreatedAt { get; }

    public bool IsDeleted { get; }
}
