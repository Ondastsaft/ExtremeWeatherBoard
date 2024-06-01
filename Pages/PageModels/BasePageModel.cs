using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages.PageModels
{
    public abstract class BasePageModel : PageModel
    {
        public SideBarPartialViewModel? SideBarOptions { get; set; }
        public MainContentViewModel? MainContent { get; set; }
    }
}
