using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}