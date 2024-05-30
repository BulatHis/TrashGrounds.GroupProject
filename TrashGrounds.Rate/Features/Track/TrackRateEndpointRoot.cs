using TrashGrounds.Rate.Features.Track.ChangeTrackRate;
using TrashGrounds.Rate.Features.Track.DeleteTrackRate;
using TrashGrounds.Rate.Features.Track.GetUserTrackRate;
using TrashGrounds.Rate.Infrastructure.Routing;

namespace TrashGrounds.Rate.Features.Track;

public class TrackRateEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/track/{trackId:guid}/rate")
            .WithTags("Оценка трека")
            .AddEndpoint<ChangeTrackRateEndpoint>()
            .AddEndpoint<GetTrackUserRateEndpoint>()
            .AddEndpoint<DeleteTrackRateEndpoint>();
    }
}