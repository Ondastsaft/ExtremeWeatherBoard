using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Services
{
    public class CommentService
    {
        private readonly DataRepository _dataRepository;
        public CommentService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public async Task<IEnumerable<Comment>> GetCommentsAsync(int discussionThreadId)
        {
            await _dataRepository.PopulateCommentsAsync();
            if (_dataRepository.Comments is List<Comment>)
            {
                List<Comment> comments = _dataRepository.Comments;
                return comments.Where(c => c.CommentThreadId == discussionThreadId).ToList();
            }
            return new List<Comment>();
        }
    }
}
