using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Post.GetPostUserRate;

public record GetPostUserRateQuery(Guid UserId, Guid PostId) : IQuery<PostUserRateResponse>;