using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Interfaces
{
    public interface IApiService
    {
        Task<List<Category>>? GetObjects();
    }
}
