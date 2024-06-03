using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ExtremeWeatherBoard.Pages
{
    using ExtremeWeatherBoard.Pages.PageModels;
    using Microsoft.AspNetCore.Identity;

    public class IndexModel : BasePageModel
    {
        private readonly CategoryService _categoryService;
        private readonly UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MockDataGenerator _dataGenerator;

        public IndexModel(CategoryService categoryService, UserDataService userDataService, UserManager<IdentityUser> usermanager, MockDataGenerator dataGenerator)
        {
            _categoryService = categoryService;
            _userDataService = userDataService;
            _userManager = usermanager;
            _dataGenerator = dataGenerator;
        }
        public async Task OnGetAsync()
        {

            //await _dataGenerator.MockLoadsOfDataAsync();

            await _userDataService.CheckCurrentUserAsync(User);
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/Categories";
            SideBarOptions.SideBarOptions = (await _categoryService.GetCategoriesAsync())
            .Cast<ISideBarOption>()
            .ToList();
        }

    }
}
