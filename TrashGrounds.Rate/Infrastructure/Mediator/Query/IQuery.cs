using MediatR;

namespace TrashGrounds.Rate.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}