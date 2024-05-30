using Grpc.Core;
using MediatR;
using PostRateServer;
using TrashGrounds.Rate.Features.Grpc.Post.GetPostRate;
using TrashGrounds.Rate.Features.Grpc.Post.GetPostsRate;

namespace TrashGrounds.Rate.gRPC.Services;

public class PostRateService : PostRateServer.PostRateService.PostRateServiceBase
{
    private readonly IMediator _mediator;

    public PostRateService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetPostRateResponse> GetPostRate(GetPostRateRequest request, ServerCallContext context)
    {
        return new GetPostRateResponse
        {
            Rating = await _mediator.Send(new GetPostRateQuery(
                Guid.Parse(request.Id)))
        };
    }

    public override async Task<GetPostsRateResponse> GetPostsRate(GetPostsRateRequest request, ServerCallContext context)
    {
        var postsRate = await _mediator.Send(new GetPostsRateQuery(
            request.Ids.Select(Guid.Parse)));

        var response = new GetPostsRateResponse();
        response.Rates.Add(postsRate.PostsRate.Select(rate => new PostRate
        {
            Id = rate.PostId.ToString(),
            Rating = rate.Rate
        }));
        return response;
    }
}