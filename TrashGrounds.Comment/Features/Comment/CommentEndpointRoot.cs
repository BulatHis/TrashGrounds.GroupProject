using TrashGrounds.Comment.Features.Comment.AddComment;
using TrashGrounds.Comment.Features.Comment.DeleteComment;
using TrashGrounds.Comment.Features.Comment.EditComment;
using TrashGrounds.Comment.Features.Comment.GetTrackComments;
using TrashGrounds.Comment.Infrastructure.Routing;

namespace TrashGrounds.Comment.Features.Comment;

public class CommentEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/track/{trackId:guid}/comments")
            .AddEndpoint<GetTrackCommentsEndpoint>()
            .AddEndpoint<AddCommentEndpoint>()
            .AddEndpoint<EditCommentEndpoint>()
            .AddEndpoint<DeleteCommentEndpoint>();
    }
}