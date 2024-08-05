using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.ViewModels.DTO
{
    public class MainContentCardWrapper
    {
        public IMainContent MainContentItem { get; set; }
        public UserData PostUserData { get; set; }
        public string RouteString { get; set; }
        public MainContentCardWrapper(IMainContent mainContentItem, UserData postUserData, string routeString)
        {
            MainContentItem = mainContentItem;
            PostUserData = postUserData;
            RouteString = routeString;
        }

    }
}
