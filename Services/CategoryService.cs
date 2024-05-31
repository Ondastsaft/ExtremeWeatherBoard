namespace ExtremeWeatherBoard.Services
{
    using ExtremeWeatherBoard.Interfaces;
    using ExtremeWeatherBoard.Models;
    using ExtremeWeatherBoard.Data;
    using System.Collections.Generic;

    public class CategoryService : ICategoryService
    {
        private readonly DataRepository _dataRepository;
        public CategoryService(DataRepository dataRepository) 
        { 
            _dataRepository = dataRepository; 
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            await _dataRepository.PopulateCategoriesAsync();
            if(_dataRepository.Categories is List<Category>)
            {
                List<Category> categories = _dataRepository.Categories;
                return categories;
            }
            return new List<Category>();
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            await _dataRepository.PopulateCategoriesAsync();
            if (_dataRepository.Categories is List<Category>)
            {
                List<Category> categories = _dataRepository.Categories;
                if (categories.Any(c => c.Id == id))
                { 
                    Category category = categories.Find(c => c.Id == id);
                    return category;
                }
            }
            return new Category();
        }
    }

}

