using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}