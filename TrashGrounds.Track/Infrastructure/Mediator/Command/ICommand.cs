using MediatR;

namespace TrashGrounds.Track.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}