using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.DTO;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class CommentsModel : BasePageModel
    {
        public DiscussionThreadDTO DiscussionThread { get; set; } = new();
        [BindProperty]
        public int DiscussionThreadId { get; set; }
        public List<CommentDTO> Comments { get; set; } = new();
        private readonly SubCategoryService _subCategoryService;
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;
        public CommentsModel(SubCategoryService subCategoryService, CommentService commentService, DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _commentService = commentService;
            _discussionThreadService = discussionThreadService;

        }
        public async Task OnGetAsync(int sidebarContentId, int discussionThreadId)
        {
            DiscussionThreadId = discussionThreadId;
            var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (subCategories != null)
            {
                SideBarOptions = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<ISideBarOption>().ToList() };
            }
            await LoadCommentsList(DiscussionThreadId);
            await LoadDiscussionThread(DiscussionThreadId);
        }
        public async Task LoadDiscussionThread(int discussionThreadId)
        {
            var discussionThread = await _discussionThreadService.GetDiscussionThreadAsync(discussionThreadId);
            if (discussionThread != null)
            {
                DiscussionThread = new DiscussionThreadDTO()
                {
                    Id = discussionThread.Id,
                    Title = discussionThread.Title == null ? "title not found": discussionThread.Title,
                    Text = discussionThread.Text == null ? "text not found" : discussionThread.Text,
                    UserDataId = discussionThread.DiscussionThreadUserDataId,
                    UserName = discussionThread.DiscussionThreadUserData?.Name ?? "User name not found",
                    TimeStamp = discussionThread.TimeStamp.ToShortTimeString(),
                    SubCategoryId = discussionThread.SubCategoryId
                };   
            }
        }

        public async Task LoadCommentsList(int discussionThreadId)
        {
            var comments = await _commentService.GetCommentsAsync(discussionThreadId);
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    if (comment.Text != null && comment.CommentUserData != null && comment.CommentUserData.Name != null)
                    {
                        Comments.Add(new CommentDTO()
                        {
                            Id = comment.Id,
                            Text = comment.Text,
                            UserDataId = comment.CommentUserDataId,
                            UserName = comment.CommentUserData.Name,
                            TimeStamp = comment.TimeStamp.ToShortTimeString(),
                            IsReported = comment.IsReported
                        });
                    }
                }
            }
        }
    }
}
