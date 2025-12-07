using System.Net.Http.Json;
using JokeApi.Models;

namespace JokeApi.Services;

public class JokeService : IJokeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public JokeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<JokeDto>> GetRandomJokesAsync(int count, CancellationToken ct = default)
    {
        var client = _httpClientFactory.CreateClient("JokeApi");

        // The official API has endpoints like /random_joke (single). We'll call it 'count' times in parallel.
        var tasks = Enumerable.Range(0, count).Select(async _ =>
        {
            try
            {
                var resp = await client.GetFromJsonAsync<JokeDto>("/random_joke", cancellationToken: ct);
                return resp;
            }
            catch
            {
                return null;
            }
        });

        var results = await Task.WhenAll(tasks);
        return results.Where(r => r != null).Select(r => r!).ToList();
    }
}
