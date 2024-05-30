namespace TrashGrounds.Rate.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}