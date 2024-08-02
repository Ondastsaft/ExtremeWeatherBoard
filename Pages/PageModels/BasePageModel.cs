using ExtremeWeatherBoard.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages.PageModels
{
    public abstract class BasePageModel : PageModel
    {
        public SideBarPartialViewModel SideBarOptions { get; set; } = new SideBarPartialViewModel();
    }
}
