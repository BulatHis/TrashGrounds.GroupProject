using System.Net;
using TrashGrounds.File.Infrastructure.Constants;

namespace TrashGrounds.File.Infrastructure.Exceptions;

public class NotFoundException<T> : DomainException
{
    public NotFoundException() : base(
        ErrorCodes.NotFoundError, (int)HttpStatusCode.NotFound)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}