using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.DTO;
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
            if (PostId != 0)
            {
                var discussionThread = await _discussionThreadService.GetDiscussionThreadAsync(PostId);
                if (discussionThread != null)
                {
                    DiscussionThread = new DiscussionThreadDTO
                    {
                        Id = discussionThread.Id,
                        Title = discussionThread.Title ?? "Title not found"
                    };
                }
            }
        }
        public async Task<IActionResult> OnPostAsync(int postId)
        {
            PostId = postId;
          if(PostId != 0 && Comment != null)
          {
           await _commentService.PostCommentAsync(PostId, Comment, User);
                return RedirectToPage("Comments", new { discussionThreadId = PostId });
          }
          return Page();
        }
    }
}
