namespace TrashGrounds.Post.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}