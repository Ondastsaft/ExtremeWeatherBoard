using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Pages.Shared.ViewModels
{
    public class MainContentViewModel
    {
        public List<IMainContent> MainContent { get; set; }
        public MainContentViewModel()
        {
            MainContent = new List<IMainContent>();
        }
    }
}
