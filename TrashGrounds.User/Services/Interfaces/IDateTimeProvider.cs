namespace TrashGrounds.User.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}