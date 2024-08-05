using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Pages.Shared.Views
{
    public class MainContentPartialViewModel
    {
        public List<IContent> MainContentList { get; set; } = new();
        public string NavigateTo { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
    }
}
