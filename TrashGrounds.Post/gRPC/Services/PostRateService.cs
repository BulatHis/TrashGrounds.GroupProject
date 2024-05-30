using PostRateClient;
using TrashGrounds.Post.Models.Main;

namespace TrashGrounds.Post.gRPC.Services;

public class PostRateService
{
    private readonly PostRateClient.PostRateService.PostRateServiceClient _client;

    public PostRateService(PostRateClient.PostRateService.PostRateServiceClient client)
    {
        _client = client;
    }

    public async Task<int?> GetPostRateAsync(Guid id)
    {
        try
        {
            return (await _client.GetPostRateAsync(new GetPostRateRequest {Id = id.ToString()})).Rating;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Rate>?> GetPostsRateAsync(IEnumerable<Guid> ids)
    {
        try
        {
            var request = new GetPostsRateRequest();
            request.Ids.Add(ids.Select(id => id.ToString()));

            var response = await _client.GetPostsRateAsync(request);

            return response.Rates.Select(rate => new Rate(Guid.Parse(rate.Id), rate.Rating));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}