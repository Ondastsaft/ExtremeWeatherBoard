using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Data.Migrations;
using ExtremeWeatherBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtremeWeatherBoard.DAL
{
    public class SubCategoryService
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>>? GetSubCategoriesFromParentIdAsync(int parentCategoryId)
        {
            var subCategories = await _context.SubCategories.Where(sc => sc.ParentCategoryId == parentCategoryId).ToListAsync();
            return subCategories;
        }
    }
}
