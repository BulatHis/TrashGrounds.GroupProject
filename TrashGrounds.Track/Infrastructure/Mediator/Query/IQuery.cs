using MediatR;

namespace TrashGrounds.Track.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}