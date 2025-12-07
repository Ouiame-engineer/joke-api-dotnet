using JokeApi.Models;

namespace JokeApi.Services;

public interface IJokeService
{
    Task<List<JokeDto>> GetRandomJokesAsync(int count, CancellationToken ct = default);
}
