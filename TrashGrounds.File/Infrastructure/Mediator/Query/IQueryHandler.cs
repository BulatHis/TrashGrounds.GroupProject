using MediatR;

namespace TrashGrounds.File.Infrastructure.Mediator.Query;

public interface IQueryHandler<in TQuery, TOut> : IRequestHandler<TQuery, TOut>
    where TQuery : IQuery<TOut>
{
    
}