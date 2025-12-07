namespace JokeApi.Models;

public class NumberRequest
{
    public string Numbers { get; set; } = string.Empty; // e.g. "5,2,10,3"
    public int Direction { get; set; } // 0 = ascending, 1 = descending
}
