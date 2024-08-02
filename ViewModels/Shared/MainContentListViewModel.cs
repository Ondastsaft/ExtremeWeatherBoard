using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;


namespace ExtremeWeatherBoard.ViewModels.Shared
{
    public class MainContentListViewModel
    {
        public IEnumerable<IMainContent> MainContentList { get; set; } = new List<IMainContent>();
    }
}
