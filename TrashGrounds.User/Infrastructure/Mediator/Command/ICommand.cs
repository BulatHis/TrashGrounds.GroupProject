using MediatR;

namespace TrashGrounds.User.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}