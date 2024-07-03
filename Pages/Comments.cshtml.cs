using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
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

            var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (subCategories !=null)
            {
                SideBarOptions = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<ISideBarOption>().ToList() };
            }
            var comments = await _commentService.GetCommentsAsync(mainContentId);
            if (comments != null)
            {
                MainContent = new MainContentViewModel()
                {
                    MainContentList = comments.ToList(),
                };
            }
        }
    }
}
