using AutoMapper;
using TrackRateClient;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.gRPC.Services;

public class TrackRateService
{
    private readonly TrackRateClient.TrackRateService.TrackRateServiceClient _client;
    private readonly IMapper _mapper;

    public TrackRateService(TrackRateClient.TrackRateService.TrackRateServiceClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Rate?> GetTrackRate(Guid id)
    {
        try
        {
            var response = await _client.GetTrackRateAsync(new GetTrackRateRequest {Id = id.ToString()});
            
            return new Rate(id, response.Rating);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Rate>?> GetTracksRate(IEnumerable<Guid> ids)
    {
        try
        {
            var request = new GetTracksRateRequest();
            request.Ids.Add(ids.Select(id => id.ToString()));

            var response = await _client.GetTracksRateAsync(request);

            return response.Rates.Select(rate => new Rate(Guid.Parse(rate.Id), rate.Rating));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Rate>?> GetBestRatedTracks(int take, int skip)
    {
        try
        {
            var response =
                await _client.GetBestRatedTrackAsync(new GetBestRatedTracksRequest {Count = take, Skip = skip});

            return response.Rates.Select(rate => _mapper.Map<Rate>(rate));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}