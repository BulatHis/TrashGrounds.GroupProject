using MediatR;

namespace TrashGrounds.Comment.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}