using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Infrastructure.ValidationSetup;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackEndpoint : IEndpoint
{
    public record AddTrackDto(string Title, string? Description,
        bool IsExplicit, Guid? PictureId, Guid MusicId, IEnumerable<Guid> Genres);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async ([FromBody] AddTrackDto dto, [FromServices] IUserService userService, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddTrackCommand(
                    userService.GetUserIdOrThrow(),
                    dto.Title,
                    dto.Description,
                    dto.IsExplicit,
                    dto.PictureId,
                    dto.MusicId,
                    dto.Genres))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<AddTrackDto>());
    }
}