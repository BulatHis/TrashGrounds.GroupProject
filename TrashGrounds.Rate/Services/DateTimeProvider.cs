using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}