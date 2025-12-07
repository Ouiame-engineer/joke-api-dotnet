using System.Diagnostics;
using JokeApi.Models;
using JokeApi.Persistence;
using JokeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NumbersController : ControllerBase
{
    private readonly INumberService _numberService;
    private readonly ILogRepository _log;

    public NumbersController(INumberService numberService, ILogRepository log)
    {
        _numberService = numberService;
        _log = log;
    }

    // POST api/numbers/sort
    [HttpPost("sort")]
    public async Task<IActionResult> Sort([FromBody] NumberRequest req)
    {
        var sw = Stopwatch.StartNew();
        var reqDesc = System.Text.Json.JsonSerializer.Serialize(req);

        try
        {
            var result = _numberService.Process(req.Numbers, req.Direction);

            sw.Stop();

            var responseJson = System.Text.Json.JsonSerializer.Serialize(result);
            await _log.AppendAsync(new RequestLog
            {
                Endpoint = "/api/numbers/sort",
                Request = reqDesc,
                Response = responseJson,
                DurationMs = sw.ElapsedMilliseconds
            });

            return Ok(result);
        }
        catch (FormatException fex)
        {
            sw.Stop();
            await _log.AppendAsync(new RequestLog
            {
                Endpoint = "/api/numbers/sort",
                Request = reqDesc,
                Response = $"ERROR: {fex.Message}",
                DurationMs = sw.ElapsedMilliseconds
            });
            return BadRequest(fex.Message);
        }
        catch (ArgumentException aex)
        {
            sw.Stop();
            return BadRequest(aex.Message);
        }
        catch (Exception ex)
        {
            sw.Stop();
            await _log.AppendAsync(new RequestLog
            {
                Endpoint = "/api/numbers/sort",
                Request = reqDesc,
                Response = $"ERROR: {ex.Message}",
                DurationMs = sw.ElapsedMilliseconds
            });
            return StatusCode(500, "Internal error");
        }
    }
}
