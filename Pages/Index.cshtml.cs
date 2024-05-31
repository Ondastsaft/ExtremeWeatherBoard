using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Services;
using ExtremeWeatherBoard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace ExtremeWeatherBoard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CategoryService _categoryService;
        [ViewData]
        public IEnumerable<Category>? SideBarOptions { get; set; }
        public IndexModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task OnGet()
        {
            SideBarOptions = await _categoryService.GetCategoriesAsync();

        }

    }    
}
