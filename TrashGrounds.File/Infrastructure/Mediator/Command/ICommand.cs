using MediatR;

namespace TrashGrounds.File.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}