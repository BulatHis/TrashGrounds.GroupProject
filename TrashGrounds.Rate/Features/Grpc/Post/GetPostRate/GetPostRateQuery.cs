using TrashGrounds.Rate.Infrastructure.Mediator.Query;

namespace TrashGrounds.Rate.Features.Grpc.Post.GetPostRate;

public record GetPostRateQuery(Guid PostId) : IQuery<int>;