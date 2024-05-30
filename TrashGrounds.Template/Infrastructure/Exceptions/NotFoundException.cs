using System.Net;
using TrashGrounds.Template.Infrastructure.Constants;

namespace TrashGrounds.Template.Infrastructure.Exceptions;

public class NotFoundException<T> : DomainException
{
    public NotFoundException() : base(
        ErrorCodes.NotFoundError, (int)HttpStatusCode.NotFound)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}