using MediatR;

namespace TrashGrounds.Post.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}