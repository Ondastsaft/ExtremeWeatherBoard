using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ExtremeWeatherBoard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ExtremeWeatherBoard.DAL
{
    public class CategoryApiService
    {
        protected Uri BaseAdress { get; }
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SubCategoryService _subCategoryService;
        public CategoryApiService(ApplicationDbContext context, UserManager<IdentityUser> userManager, SubCategoryService subCategoryService)
        {
            BaseAdress = new Uri("https://extremeweatherboardapi.azurewebsites.net");
            _context = context;
            _userManager = userManager;
            _subCategoryService = subCategoryService;            
        }
#if DEBUG
        public async Task<List<Category>?> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        internal async Task<List<Category>> GetCategoriesWithSubCategoriesAsync()
        {
            var categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
            return categories;
        }
#else
        public async Task<List<Category>?> GetCategoriesWithSubCategoriesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;
                HttpResponseMessage response = await client.GetAsync("/api/Category/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Category>? retrievedObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(responseString);
                        if (retrievedObjects != null && retrievedObjects.Count > 0)
                        {
                            return retrievedObjects;
                        }
                        if(retrievedObjects != null && retrievedObjects.Count == 0)
                        {
                            foreach (var category in retrievedObjects)
                            {
                                var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(category.Id);
                                if (subCategories != null)
                                {
                                    category.SubCategories = subCategories;
                                }
                            }
                        }
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                return null;
            }
        }
#endif

        internal async Task<Category?> GetCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }

    }
}
