using JokeApi.Services;

public class NumberServiceTests
{
    private readonly NumberService _service = new();

    [Fact]
    public void Process_Ascending_Works()
    {
        var res = _service.Process("5,2,10,3", 0);
        Assert.Equal(new[] { 2, 3, 5, 10 }, res.Ordered);
        Assert.Equal(20, res.Sum);
    }

    [Fact]
    public void Process_Descending_Works()
    {
        var res = _service.Process("5,1,10,3", 1);
        Assert.Equal(new[] { 10, 5, 3, 1 }, res.Ordered);
        Assert.Equal(19, res.Sum);
    }

    [Fact]
    public void Process_InvalidNumber_Throws()
    {
        Assert.Throws<FormatException>(() => _service.Process("5,abc,2", 0));
    }
}
