using MediatR;

namespace TrashGrounds.User.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}