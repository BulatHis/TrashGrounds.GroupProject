namespace TrashGrounds.File.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}