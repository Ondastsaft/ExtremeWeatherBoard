using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Shared.Views
{
    public class MainContentCardItemPartialViewModel
    {
        public IContent MainContentItem { get; set; }
        public string RouteString { get; set; }
        public MainContentCardItemPartialViewModel(IContent mainContentItem, string routeString)
        {
            MainContentItem = mainContentItem;
            RouteString = routeString;
        }
    }
}
