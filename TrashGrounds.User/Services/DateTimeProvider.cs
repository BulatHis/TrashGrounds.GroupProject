using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}