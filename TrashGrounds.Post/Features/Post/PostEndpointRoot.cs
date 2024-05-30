using TrashGrounds.Post.Features.Post.AddPost;
using TrashGrounds.Post.Features.Post.DeletePost;
using TrashGrounds.Post.Features.Post.EditPost;
using TrashGrounds.Post.Features.Post.GetUserPosts;
using TrashGrounds.Post.Features.Post.MyPosts;
using TrashGrounds.Post.Infrastructure.Routing;

namespace TrashGrounds.Post.Features.Post;

public class PostEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/posts")
            .AddEndpoint<AddPostEndpoint>()
            .AddEndpoint<EditPostEndpoint>()
            .AddEndpoint<DeletePostEndpoint>()
            .AddEndpoint<GetUserPostsEndpoint>()
            .AddEndpoint<GetMyPostsEndpoint>();
    }
}