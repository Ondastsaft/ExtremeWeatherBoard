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
            
            var subCategories = await _subCategoryService.GetSubCategoriesAsync(sidebarContentId);
            if (subCategories is IEnumerable<SubCategory>) 
            {
                SideBarOptions = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<ISideBarOption>().ToList() };               
            }

            var discussionThreads = await _discussionThreadService.GetThreadsAsync(mainContentId);
            var discussionParentThread = discussionThreads.Where(dt => dt.Id == mainContentId).FirstOrDefault();
            var comments = await _commentService.GetCommentsAsync(mainContentId);
            if (discussionParentThread is DiscussionThread && comments is IEnumerable<Comment>)
            {
                MainContent = new MainContentViewModel()
                {
                    MainContentList = comments.Cast<IMainContent>().ToList(),
                    CommentsParentDiscussionThread = discussionParentThread
                };
            }
            //MainContent.MainContentList = (await _commentService.GetCommentsAsync(mainContentId))
            //    .Cast<IMainContent>().
            //    ToList();
            //MainContent.CommentsParentDiscussionThread = await _discussionThreadService.GetThreadAsync(mainContentId);
        }
    }
}
