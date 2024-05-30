namespace TrashGrounds.Comment.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}