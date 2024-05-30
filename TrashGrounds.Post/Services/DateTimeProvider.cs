using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}