using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.ViewModels.Shared
{
    public class MainContentCardsViewModel
    {
        public List<IMainContent> MainContentList { get; set; } = new List<IMainContent>();
        public string RouteString { get; set; } = "";

    }
}
