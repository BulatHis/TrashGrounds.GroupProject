using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}