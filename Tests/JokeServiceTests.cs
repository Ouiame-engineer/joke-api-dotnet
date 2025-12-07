using System.Net;
using System.Text;
using System.Text.Json;
using JokeApi.Models;
using JokeApi.Services;
public class JokeServiceTests
{
    [Fact]
    public async Task GetRandomJokesAsync_ReturnsRequestedNumberOfJokes()
    {
        // Arrange
        var jokes = new[]
        {
            new JokeDto { Id = 1, Type = "general", Setup = "Setup 1", Punchline = "Punchline 1" },
            new JokeDto { Id = 2, Type = "general", Setup = "Setup 2", Punchline = "Punchline 2" }
        };

        var handler = new FakeHttpMessageHandler(jokes);
        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://fake-joke-api")
        };

        var factory = new FakeHttpClientFactory(client);
        var service = new JokeService(factory);

        // Act
        var result = await service.GetRandomJokesAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, joke =>
        {
            Assert.False(string.IsNullOrWhiteSpace(joke.Setup));
            Assert.False(string.IsNullOrWhiteSpace(joke.Punchline));
        });
    }
}

internal class FakeHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClient _client;

    public FakeHttpClientFactory(HttpClient client)
    {
        _client = client;
    }

    public HttpClient CreateClient(string name) => _client;
}

internal class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly Queue<JokeDto> _responses;

    public FakeHttpMessageHandler(IEnumerable<JokeDto> jokes)
    {
        _responses = new Queue<JokeDto>(jokes);
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_responses.Count == 0)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        }

        var joke = _responses.Dequeue();
        var json = JsonSerializer.Serialize(joke);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        return Task.FromResult(response);
    }
}
