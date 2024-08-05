using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Pages.Shared.Views
{
    public class SideBarPartialViewModel
    {
        public List<IContent>? SideBarOptions { get; set; }
        public string NavigateTo { get; set; }
        public SideBarPartialViewModel()
        {
            NavigateTo = "";
        }
    }
}
