using TrashGrounds.Post.Infrastructure.Mediator.Query;
using TrashGrounds.Post.Models.Additional;

namespace TrashGrounds.Post.Features.Post.GetUserPosts;

public record GetUserPostsQuery(
    Guid UserId,
    int Take,
    int Skip,
    bool ShowHidden = false) : IQuery<IEnumerable<PostWithRate>>;