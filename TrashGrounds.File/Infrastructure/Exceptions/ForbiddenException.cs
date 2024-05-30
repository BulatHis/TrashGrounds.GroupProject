using System.Net;

namespace TrashGrounds.File.Infrastructure.Exceptions;

public class ForbiddenException: DomainException
{
    public ForbiddenException(string message) : base(message, (int)HttpStatusCode.Forbidden)
    {
    }
}
