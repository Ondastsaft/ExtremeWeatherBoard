using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.ViewModels.Shared
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
