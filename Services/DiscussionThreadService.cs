using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Services
{
    public class DiscussionThreadService
    {
        private readonly DataRepository _dataRepository;
        public DiscussionThreadService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public async Task<IEnumerable<DiscussionThread>> GetThreadsAsync(int subCategoryId)
        {
            await _dataRepository.PopulateDiscussionThreadsAsync();
            if (_dataRepository.DiscussionThreads is List<DiscussionThread>)
            {
                List<DiscussionThread> threads = _dataRepository.DiscussionThreads;
                return threads.Where(t => t.SubCategoryId == subCategoryId).ToList();
            }
            return new List<DiscussionThread>();
        }
        public async Task<DiscussionThread?> GetThreadAsync(int threadId)
        {
            var threads = new List<DiscussionThread>();
             await _dataRepository.PopulateDiscussionThreadsAsync();
            if(_dataRepository.DiscussionThreads is IEnumerable<DiscussionThread>)
            {
                threads = _dataRepository.DiscussionThreads.ToList();
            }

            if (threads is List<DiscussionThread>)
            {
                if (threads.Any(dt => dt.Id == threadId))
                {
                    DiscussionThread? thread = threads.Where(dt => dt.Id == threadId).FirstOrDefault();
                    if(thread is DiscussionThread)
                    return thread;
                }
            }
            return null;
                //new DiscussionThread() { 
                //Id = 0, Title = "0", Text = "0", 
                //Comments = new List<Comment>(), 
                //CreatorUserId = 0, 
                //CreatedAt = new DateTime(), 
                //SubCategoryId = 0}   ;
        }
    }
}
