using System.Net;
using TrashGrounds.Post.Infrastructure.Constants;

namespace TrashGrounds.Post.Infrastructure.Exceptions;

public class NotFoundException<T> : DomainException
{
    public NotFoundException() : base(
        ErrorCodes.NotFoundError, (int)HttpStatusCode.NotFound)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}