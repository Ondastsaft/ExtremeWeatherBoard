using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtremeWeatherBoard.DAL
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetCommentsAsync(int discussionThreadId)
        {
            List<Comment> comments = await _context.Comments.Where(c => c.CommentThreadId == discussionThreadId).ToListAsync();
            return comments;
        }
    }
}
