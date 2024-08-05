using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.Shared.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages.PageModels
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly UserDataService _userDataService;
        public SideBarPartialViewModel PageSideBarPartialModel { get; set; } = new();
        public MainContentPartialViewModel PageMainContentPartialModel { get; set; } = new MainContentPartialViewModel();
        protected int MainContentId { get; set; }
        protected int SideBarContentId { get; set; }
        public BasePageModel(UserDataService userDataService)
        {
         _userDataService = userDataService;
        }
        public virtual async Task OnGetAsync(int maincontentId = 0, int sideBarContentId = 0)
        {
            await _userDataService.CheckCurrentUserAsync(User);
            MainContentId = maincontentId;
            SideBarContentId = sideBarContentId;
            await LoadSideBar();
            await LoadMainContent();
        }
        protected abstract Task LoadSideBar();
        protected abstract Task LoadMainContent();

    }
}
