using Grpc.Core;
using MediatR;
using TrackRateServer;
using TrashGrounds.Rate.Features.Grpc.Track.GetBestRatedTracks;
using TrashGrounds.Rate.Features.Grpc.Track.GetTrackRate;
using TrashGrounds.Rate.Features.Grpc.Track.GetTracksRate;

namespace TrashGrounds.Rate.gRPC.Services;

public class TrackRateService : TrackRateServer.TrackRateService.TrackRateServiceBase
{
    private readonly IMediator _mediator;

    public TrackRateService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetTrackRateResponse> GetTrackRate(GetTrackRateRequest request, ServerCallContext context)
    {
        var rate = await _mediator.Send(new GetTrackRateQuery(
            Guid.Parse(request.Id)));
        return new GetTrackRateResponse
        {
            Rating = rate
        };
    }

    public override async Task<GetTracksRateResponse> GetTracksRate(GetTracksRateRequest request, ServerCallContext context)
    {
        var tracksRate = await _mediator.Send(new GetTracksRateQuery(
            request.Ids.Select(Guid.Parse)));

        var response = new GetTracksRateResponse();
        response.Rates.Add(tracksRate.TracksRate.Select(rate => new TrackRate
        {
            Id = rate.TrackId.ToString(),
            Rating = rate.Rate
        }));
        return response;
    }

    public override async Task<GetBestRatedTracksResponse> GetBestRatedTrack(GetBestRatedTracksRequest request, ServerCallContext context)
    {
        var tracksRate = await _mediator.Send(new GetBestRatedTracksQuery(request.Take, request.Skip));
        
        var response = new GetBestRatedTracksResponse();
        response.Rates.Add(tracksRate.TracksRate.Select(rate => new TrackRate
        {
            Id = rate.TrackId.ToString(),
            Rating = rate.Rate
        }));
        return response;
    }
}