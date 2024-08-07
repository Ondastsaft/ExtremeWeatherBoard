﻿using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace ExtremeWeatherBoard.DAL
{
    public class SubCategoryService
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetSubCategoriesAsync()
        {
            var subCategories = await _context.SubCategories.ToListAsync();
            return subCategories;
        }
        public async Task<SubCategory> GetSubCategory (int subCategoryId) 
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
            if (subCategory != null)
            {
                return subCategory;
            }
            else return new SubCategory();
        }
        public async Task<List<SubCategory>> GetSubCategoriesFromParentIdAsync(int parentCategoryId)
        {
            var subCategories = await _context.SubCategories.Where(sc => sc.ParentCategoryId == parentCategoryId).ToListAsync();
            return subCategories;
        }
    }
}
