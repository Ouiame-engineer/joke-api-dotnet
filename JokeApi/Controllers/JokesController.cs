using System.Diagnostics;
using JokeApi.Persistence;
using JokeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class JokesController : ControllerBase
{
    private readonly IJokeService _jokeService;
    private readonly ILogRepository _log;

    public JokesController(IJokeService jokeService, ILogRepository log)
    {
        _jokeService = jokeService;
        _log = log;
    }

    // GET api/jokes?count=5
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int count = 1, CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();
        var reqDesc = $"count={count}";

        try
        {
            if (count <= 0 || count > 50) // protect API
                return BadRequest("count must be between 1 and 50");

            var jokes = await _jokeService.GetRandomJokesAsync(count, ct);

            sw.Stop();

            var responseJson = System.Text.Json.JsonSerializer.Serialize(jokes);
            await _log.AppendAsync(new RequestLog
            {
                Endpoint = "/api/jokes",
                Request = reqDesc,
                Response = responseJson,
                DurationMs = sw.ElapsedMilliseconds
            });

            return Ok(jokes);
        }
        catch (OperationCanceledException)
        {
            sw.Stop();
            return StatusCode(499); // client closed request
        }
        catch (Exception ex)
        {
            sw.Stop();
            await _log.AppendAsync(new RequestLog
            {
                Endpoint = "/api/jokes",
                Request = reqDesc,
                Response = $"ERROR: {ex.Message}",
                DurationMs = sw.ElapsedMilliseconds
            });

            return StatusCode(503, "External joke provider error or internal error");
        }
    }
}
