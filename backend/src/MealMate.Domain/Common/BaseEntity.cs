namespace MealMate.Domain.Common;
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt{ get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public string? Logo { get; protected set; }
}