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
    }
}
