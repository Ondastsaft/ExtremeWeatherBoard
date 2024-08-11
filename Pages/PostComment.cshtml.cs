using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.DTO;
using System.Globalization;
namespace ExtremeWeatherBoard.Pages
{
    public class PostCommentModel : PageModel
    {
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;
        [BindProperty]
        public DiscussionThreadDTO DiscussionThread { get; set; } = new();
        [BindProperty]
        public int PostId { get; set; }
        [BindProperty]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Comment can't exceed 1000 characters")]
        public string Comment { get; set; } = string.Empty;
        public PostCommentModel(CommentService commentService, DiscussionThreadService discussionThreadService)
        {
            _commentService = commentService;
            _discussionThreadService = discussionThreadService;
        }
        public async Task OnGet(int postId)
        {
            PostId = postId;
            await LoadDiscussionThread(postId);

        }
        private async Task LoadDiscussionThread(int discussionThreadId)
        {
            var discussionThread = await _discussionThreadService.GetDiscussionThreadAsync(discussionThreadId);
            if (discussionThread != null)
            {
                DiscussionThread = new DiscussionThreadDTO()
                {
                    Id = discussionThread.Id,
                    Title = discussionThread.Title == null ? "title not found" : discussionThread.Title,
                    Text = discussionThread.Text == null ? "text not found" : discussionThread.Text,
                    UserDataId = discussionThread.DiscussionThreadUserDataId,
                    UserName = discussionThread.DiscussionThreadUserData?.Name ?? "User name not found",
                    ImageUrl = discussionThread.DiscussionThreadUserData?.ImageURL ?? "User image URL not found",
                    TimeStamp = discussionThread.TimeStamp.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture),
                    SubCategoryId = discussionThread.SubCategoryId
                };
            }
        }
        public async Task<IActionResult> OnPostAsync(int postId)
        {
            PostId = postId;
            if (PostId != 0 && Comment != null)
            {
                await _commentService.PostCommentAsync(PostId, Comment, User);
                return RedirectToPage("Comments", new { discussionThreadId = PostId });
            }
            return Page();
        }
    }
}
