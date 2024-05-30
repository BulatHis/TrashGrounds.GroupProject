using MediatR;

namespace TrashGrounds.Rate.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}