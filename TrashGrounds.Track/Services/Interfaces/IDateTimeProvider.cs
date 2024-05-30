namespace TrashGrounds.Track.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}