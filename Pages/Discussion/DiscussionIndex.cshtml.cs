using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages.Discussion
{
    public class DiscussionIndexModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;

        public DiscussionIndexModel(
            UserDataService userDataService,
            SubCategoryService subCategoryService,
            CommentService commentService,
            DiscussionThreadService discussionThreadService
            ) : base(userDataService)
        {
            _subCategoryService = subCategoryService;
            _commentService = commentService;
            _discussionThreadService = discussionThreadService;
        }
        protected override async Task LoadMainContent()
        {
            var comments = await _commentService.GetCommentsAsync(MainContentId);
            if (comments != null)
            {
                var parentDiscussionThread = await _discussionThreadService.GetDiscussionThreadAsync(MainContentId);
                {
                    PageMainContentPartialModel.MainContentList = comments.Count > 0 ? comments.Cast<IContent>().ToList() : new List<IContent>();
                };
            }
        }
        protected override async Task LoadSideBar()
        {

            var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(SideBarContentId);
            if (subCategories != null)
            {
                PageSideBarPartialModel = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<IContent>().ToList() };
            }
        }
    }
}
