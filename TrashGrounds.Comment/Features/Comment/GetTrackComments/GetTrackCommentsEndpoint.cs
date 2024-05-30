using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Comment.Infrastructure.Routing;

namespace TrashGrounds.Comment.Features.Comment.GetTrackComments;

public class GetTrackCommentsEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async ([FromRoute] Guid trackId, int take, int? skip, IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetTrackCommentsQuery(
                trackId,
                take,
                skip ?? 0))));
    }
}