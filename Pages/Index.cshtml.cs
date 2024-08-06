using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;


namespace ExtremeWeatherBoard.Pages
{
    using ExtremeWeatherBoard.DAL;
    using ExtremeWeatherBoard.Models;
    using ExtremeWeatherBoard.Pages.PageModels;
    using Microsoft.AspNetCore.Identity;


    public class IndexModel : BasePageModel
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MockDataGenerator _dataGenerator;
        private static Uri BaseAdress = new Uri("https://localhost:44311/api/");

        public IndexModel(  
            CategoryApiService categoryApiService 
            ,UserDataService userDataService 
            ,UserManager<IdentityUser> usermanager
            , MockDataGenerator dataGenerator
            )
        {
            _categoryApiService = categoryApiService;
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
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null)
            {
                SideBarOptions.SideBarOptions = categories.Cast<ISideBarOption>().ToList();
            }
        }

    }
}
