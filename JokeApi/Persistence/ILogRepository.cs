namespace JokeApi.Persistence;

public interface ILogRepository
{
    Task AppendAsync(RequestLog log);
    Task<IReadOnlyList<RequestLog>> GetAllAsync();
}
