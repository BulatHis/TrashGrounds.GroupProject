using MediatR;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.GetPostsUserRates;

public class GetPostsUserRatesEndpoint : IEndpoint
{
    private record GetPostsUserRatesDto(IEnumerable<Guid> PostsId)
    {
        public static ValueTask<GetPostsUserRatesDto> BindAsync(HttpContext context)
        {
            var queries = context.Request.Query[nameof(PostsId)];
            var result = new GetPostsUserRatesDto(queries.Select(Guid.Parse!).ToList());
            return ValueTask.FromResult(result);
        }
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/from-posts",
                async (GetPostsUserRatesDto dto, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new GetPostsUserRatesQuery(
                            userService.GetUserIdOrThrow(),
                            dto.PostsId))))
            .RequireAuthorization();
    }
}