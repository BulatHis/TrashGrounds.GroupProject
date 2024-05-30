using MediatR;

namespace TrashGrounds.Comment.Infrastructure.Mediator.Query;

public interface IQueryHandler<in TQuery, TOut> : IRequestHandler<TQuery, TOut>
    where TQuery : IQuery<TOut>
{
    
}