namespace JokeApi.Persistence;

public class RequestLog
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Endpoint { get; set; } = string.Empty;
    public string Request { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public long DurationMs { get; set; }
}
