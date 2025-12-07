using JokeApi.Models;
using System.Diagnostics;

namespace JokeApi.Services;

public class NumberService : INumberService
{
    public NumberResponse Process(string numbersCsv, int direction)
    {
        if (string.IsNullOrWhiteSpace(numbersCsv))
            throw new ArgumentException("numbersCsv empty");

        // split and parse
        var parts = numbersCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var ints = new List<int>();
        foreach (var p in parts)
        {
            if (!int.TryParse(p, out var v))
                throw new FormatException($"Invalid number: '{p}'");
            ints.Add(v);
        }

        IEnumerable<int> ordered;
        if (direction == 0)
            ordered = ints.OrderBy(x => x);
        else
            ordered = ints.OrderByDescending(x => x);

        var response = new NumberResponse
        {
            Ordered = ordered.ToList(),
            Sum = ints.Sum()
        };
        return response;
    }
}
