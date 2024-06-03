using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class CommentsModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;
        public CommentsModel(SubCategoryService subCategoryService, CommentService commentService, DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _commentService = commentService;
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
            MainContent.MainContentList = (await _commentService.GetCommentsAsync(mainContentId))
                .Cast<IMainContent>().
                ToList();
            MainContent.CommentsParentDiscussionThread = await _discussionThreadService.GetThreadAsync(mainContentId);
        }
    }
}
