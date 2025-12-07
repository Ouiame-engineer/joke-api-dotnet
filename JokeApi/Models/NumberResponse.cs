namespace JokeApi.Models;

public class NumberResponse
{
    public List<int> Ordered { get; set; } = new();
    public int Sum { get; set; }
}
