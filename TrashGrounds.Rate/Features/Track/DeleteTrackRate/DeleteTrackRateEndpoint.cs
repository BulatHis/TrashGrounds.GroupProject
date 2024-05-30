using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.DeleteTrackRate;

public class DeleteTrackRateEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/",
                async ([FromRoute] Guid trackId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeleteTrackRateCommand(
                        userService.GetUserIdOrThrow(),
                        trackId))))
            .RequireAuthorization();
    }
}