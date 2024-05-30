using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Grpc.Post.GetPostsRate;

public record GetPostsRateQuery(IEnumerable<Guid> Ids) : IQuery<PostsRateResponse>;