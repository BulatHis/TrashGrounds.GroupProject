using Grpc.Net.ClientFactory;
using UsedFilesClient;

namespace TrashGrounds.File.gRPC.Services;

public class UsedFilesService
{
    private readonly UsedFilesClient.UsedFilesService.UsedFilesServiceClient _usedUserClient;
    private readonly UsedFilesClient.UsedFilesService.UsedFilesServiceClient _usedPostClient;
    private readonly UsedFilesClient.UsedFilesService.UsedFilesServiceClient _usedTrackClient;

    public UsedFilesService(GrpcClientFactory factory)
    {
        _usedUserClient =
            factory.CreateClient<UsedFilesClient.UsedFilesService.UsedFilesServiceClient>("UsedUser");

        _usedPostClient =
            factory.CreateClient<UsedFilesClient.UsedFilesService.UsedFilesServiceClient>("UsedPost");
        
        _usedTrackClient =
            factory.CreateClient<UsedFilesClient.UsedFilesService.UsedFilesServiceClient>("UsedTrack");
    }

    public async Task<List<Guid>> GetUsedImageIdsAsync()
    {
        var userResponse = await _usedUserClient.GetUsedImagesIdsAsync(new UsedImagesRequest());
        var postResponse = await _usedPostClient.GetUsedImagesIdsAsync(new UsedImagesRequest());
        var trackResponse = await _usedTrackClient.GetUsedImagesIdsAsync(new UsedImagesRequest());

        return ConvertResponsesToList(userResponse, postResponse, trackResponse);
    }

    public async Task<List<Guid>> GetUsedTrackIdsAsync()
    {
        var trackResponse = await _usedTrackClient.GetUsedTracksIdsAsync(new UsedTracksRequest());

        return ConvertResponsesToList(trackResponse);
    }

    private static List<Guid> ConvertResponsesToList(params UsedImagesResponse?[] responses)
    {
        return responses
            .Select(response => response?.Ids.Select(Guid.Parse))
            .Where(guids => guids != null)
            .SelectMany(x => x!)
            .Distinct()
            .ToList();
    }
    
    private static List<Guid> ConvertResponsesToList(params UsedTracksResponse?[] responses)
    {
        return responses
            .Select(response => response?.Ids.Select(Guid.Parse))
            .Where(guids => guids != null)
            .SelectMany(x => x!)
            .Distinct()
            .ToList();
    }
}