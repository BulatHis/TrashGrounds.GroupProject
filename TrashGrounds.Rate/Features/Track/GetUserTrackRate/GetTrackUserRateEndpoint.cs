using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.GetUserTrackRate;

public class GetTrackUserRateEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/",
                async ([FromRoute] Guid trackId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new GetTrackUserRateQuery(
                        userService.GetUserIdOrThrow(),
                        trackId))))
            .RequireAuthorization();
    }
}