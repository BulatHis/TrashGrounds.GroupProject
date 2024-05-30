using MediatR;

namespace TrashGrounds.Post.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}