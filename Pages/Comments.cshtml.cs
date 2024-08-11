using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.DTO;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace ExtremeWeatherBoard.Pages
{
    public class CommentsModel : BasePageModel
    {
        public DiscussionThreadDTO DiscussionThread { get; set; } = new();
        [BindProperty]
        public int DiscussionThreadId { get; set; }
        public int ReportedCommentId { get; set; }
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
        public async Task OnGetAsync(int sidebarContentId, int discussionThreadId, int reportedCommentId)
        {
            ReportedCommentId = reportedCommentId;
            DiscussionThreadId = discussionThreadId;
            var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (subCategories != null)
            {
                SideBarOptions = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<ISideBarOption>().ToList() };
            }
            await LoadCommentsList(DiscussionThreadId);
            await LoadDiscussionThread(DiscussionThreadId);
        }
        public async Task<IActionResult> OnPostReportCommentAsync(int reportedCommentId, int discussionThreadId)
        {
            var result = await ReportComment(reportedCommentId);
            if (result)
            {
                TempData["SuccessMessage"] = "Comment reported successfully.";
            }
            else TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToPage("/Comments", new {discussionThreadId = discussionThreadId, reportedCommentId = reportedCommentId });
        }
        private async Task<bool> ReportComment(int commentId)
        {
            await _commentService.ReportComment(commentId);
            var reportedComment  = await _commentService.GetCommentFromId(commentId);
            if (reportedComment != null && reportedComment.IsReported)
            {
                return true;
            }
            else return false;
        }
        private async Task LoadDiscussionThread(int discussionThreadId)
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
                    ImageUrl = discussionThread.DiscussionThreadUserData?.ImageURL ?? "User image URL not found",
                    TimeStamp = discussionThread.TimeStamp.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture),
                    SubCategoryId = discussionThread.SubCategoryId
                };   
            }
        }

        private async Task LoadCommentsList(int discussionThreadId)
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
                            ImageURL = comment.CommentUserData.ImageURL ?? "ImageUrlNotFound",
                            UserName = comment.CommentUserData.Name,
                            TimeStamp = comment.TimeStamp.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture),
                            IsReported = comment.IsReported
                        });
                    }
                }
            }
        }
    }
}
