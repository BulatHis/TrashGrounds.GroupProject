using TrashGrounds.Template.Services.Interfaces;

namespace TrashGrounds.Template.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}