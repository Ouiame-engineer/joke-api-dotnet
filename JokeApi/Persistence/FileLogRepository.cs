using System.Text.Json;

namespace JokeApi.Persistence;

public class FileLogRepository : ILogRepository
{
    private readonly string _filePath;

    public FileLogRepository()
    {
        var folder = Path.Combine(AppContext.BaseDirectory, "logs");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        _filePath = Path.Combine(folder, "requests.jsonl"); // JSON lines
    }

    public async Task AppendAsync(RequestLog log)
    {
        var json = JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = false });
        await File.AppendAllTextAsync(_filePath, json + Environment.NewLine);
    }

    public async Task<IReadOnlyList<RequestLog>> GetAllAsync()
    {
        if (!File.Exists(_filePath)) return Array.Empty<RequestLog>();
        var lines = await File.ReadAllLinesAsync(_filePath);
        var list = new List<RequestLog>();
        foreach (var line in lines)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<RequestLog>(line);
                if (obj != null) list.Add(obj);
            }
            catch { /* ignore corrupt lines */ }
        }
        return list;
    }
}
