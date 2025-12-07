using JokeApi.Models;

namespace JokeApi.Services;

public interface INumberService
{
    NumberResponse Process(string numbersCsv, int direction);
}
