using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtremeWeatherBoard.DAL
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserDataService _userDataService;
        public CommentService(ApplicationDbContext context, UserDataService userDataService)
        {
            _context = context;
            _userDataService = userDataService;
        }
        public async Task<List<Comment>?> GetCommentsRelatedToUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userData = await _userDataService.GetCurrentUserDataAsync(userPrincipal);
            if (userData != null)
            {
                List<Comment> comments = await _context.Comments.Where(c => c.CommentUserData == userData).ToListAsync();
                return comments;
            }
            return null;
        }

        public async Task<List<Comment>> GetCommentsAsync(int discussionThreadId)
        {
            List<Comment> comments = await _context.Comments.Include(c => c.CommentUserData).Where(c => c.ParentDiscussionThreadId == discussionThreadId).ToListAsync();
            return comments;
        }
        public async Task PostCommentAsync(int discussionThreadId, string text, ClaimsPrincipal userPrincipal)
        {
            UserData userData = await _userDataService.GetCurrentUserDataAsync(userPrincipal);
            var discussionThread = await _context.DiscussionThreads.FirstOrDefaultAsync(dt => dt.Id == discussionThreadId);
            if (discussionThread is DiscussionThread)
            {
                Comment postedComment = new Comment()
                {
                    Title = "",
                    CommentUserData = userData,
                    ParentDiscussionThread = discussionThread,
                    Text = text,
                    TimeStamp = DateTime.UtcNow
                };
                await _context.Comments.AddAsync(postedComment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task ReportComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment != null) 
            { 
            comment.IsReported =  true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
