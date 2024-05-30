using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Post.GetPostsUserRates;

public record GetPostsUserRatesQuery(Guid UserId, IEnumerable<Guid> PostsId) : IQuery<PostsUserRateResponse>;