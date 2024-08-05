using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class DiscussionThreadsModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        public DiscussionThreadsModel(SubCategoryService subCategoryService, DAL.DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
        }
        public async Task OnGetAsync(int sidebarContentId, int mainContentId)
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/DiscussionThreads";
            var sidebarOptions = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (sidebarOptions != null)
            {
                SideBarOptions.SideBarOptions = sidebarOptions.Cast<ISideBarOption>().ToList();
            }
            MainContent = new MainContentViewModel();
            MainContent.MainContentList = (await _discussionThreadService.GetDiscussionThreadsAsync(mainContentId))
                .Cast<IMainContent>().
                ToList();
        }
    }
}
