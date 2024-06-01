using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ExtremeWeatherBoard.Pages
{
    using ExtremeWeatherBoard.Pages.PageModels;
    public class IndexModel : BasePageModel
    {
        private readonly CategoryService _categoryService;

        public IndexModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task OnGetAsync()
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.SideBarOptions = (await _categoryService.GetCategoriesAsync())
            .Cast<ISideBarOption>()
            .ToList();
        }

    }
}
