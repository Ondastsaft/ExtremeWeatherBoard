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
        public DiscussionThreadsModel(SubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        public async Task OnGetAsync(int sideBarId, int mainContentId)
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.SideBarOptions = (await _subCategoryService.GetSubCategoriesAsync(sideBarId))
                .Cast<ISideBarOption>()
                .ToList();
            MainContent = new MainContentViewModel();
            MainContent.MainContent = (await _discussionThreadService.GetThreadsAsync(mainContentId))
                .Cast<IMainContent>().
                ToList();
            

        }
    }
}
