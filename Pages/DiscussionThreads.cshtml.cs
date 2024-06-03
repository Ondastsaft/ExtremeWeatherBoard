using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class DiscussionThreadsModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        public DiscussionThreadsModel(SubCategoryService subCategoryService, DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
        }
        public async Task OnGetAsync(int sidebarContentId, int mainContentId)
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/DiscussionThreads";
            SideBarOptions.SideBarOptions = (await _subCategoryService.GetSubCategoriesAsync(sidebarContentId))
                .Cast<ISideBarOption>()
                .ToList();
            MainContent = new MainContentViewModel();
            MainContent.MainContentList = (await _discussionThreadService.GetThreadsAsync(mainContentId))
                .Cast<IMainContent>().
                ToList();
            int i = 0;

        }
    }
}
