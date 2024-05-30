using MediatR;

namespace TrashGrounds.Comment.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}