using MediatR;

namespace TrashGrounds.File.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}