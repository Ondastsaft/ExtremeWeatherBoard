using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Services;
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
        private readonly CategoryAPIService _categoryApiService;
        private readonly Services.UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly MockDataGenerator _dataGenerator;
        private static Uri BaseAdress = new Uri("https://localhost:44311/api/");

        public IndexModel(  CategoryAPIService categoryApiService,
                            Services.UserDataService userDataService, 
                            UserManager<IdentityUser> usermanager
                            //MockDataGenerator dataGenerator
                            )

        {
            _categoryApiService = categoryApiService;
            _userDataService = userDataService;
            _userManager = usermanager;
            //_dataGenerator = dataGenerator;

        }
        public async Task OnGetAsync()
        {
            //await _dataGenerator.MockLoadsOfDataAsync()
            //await _userDataService.CheckCurrentUserAsync(User);
            //SideBarOptions = new SideBarPartialViewModel();
            //SideBarOptions.NavigateTo = "/Categories";
            //var categories = await _categoryApiService.GetObjects();
            //SideBarOptions.SideBarOptions = (await _categoryApiService.GetObjects())
            //.Cast<ISideBarOption>()
            //.ToList();
            var somtehing = 1;
        }

    }
}
