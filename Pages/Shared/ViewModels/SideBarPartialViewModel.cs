using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Pages.Shared.ViewModels
{
    public class SideBarPartialViewModel
    {
        public List<ISideBarOption>? SideBarOptions { get; set; }
        public string NavigateTo { get; set; }
        public SideBarPartialViewModel()
        {
            NavigateTo = "";
        }
    }
}
