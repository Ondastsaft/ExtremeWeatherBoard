using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Services
{
    public class SubCategoryService
    {
        private readonly DataRepository _dataRepository;
        public SubCategoryService(DataRepository dataRepository)
        {           
            _dataRepository = dataRepository;
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(int categoryId)
        {
            await _dataRepository.PopulateSubCategoriesAsync();
            if (_dataRepository.SubCategories is List<SubCategory>)
            {
                List<SubCategory> subCategories = _dataRepository.SubCategories;
                return subCategories.Where(sc => sc.ParentCategoryId == categoryId);
            }
            return new List<SubCategory>();
        }
    }
}
