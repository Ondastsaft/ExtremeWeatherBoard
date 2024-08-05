using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Interfaces
{
    public interface IContent
    {
        int Id { get; set; }
        string? Title { get; set; }
    }
}
