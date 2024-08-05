using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Data;
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
        private readonly SubCategoryService _subCategoryService;
        private readonly MockDataGenerator _dataGenerator;
        private static Uri BaseAdress = new Uri("https://localhost:44311/api/");
        public List<IMainContent> MainContent { get; set; } = new List<IMainContent>();
        public int SubCategoryId { get; set; }

        public IndexModel(  
            CategoryApiService categoryApiService
            ,UserDataService userDataService 
            ,UserManager<IdentityUser> usermanager,
            SubCategoryService subCategoryService
            , MockDataGenerator dataGenerator, int subCategoryId
            )
        {
            _categoryApiService = categoryApiService;
            _userDataService = userDataService;
            _userManager = usermanager;
            _dataGenerator = dataGenerator;
            _subCategoryService = subCategoryService;
            SubCategoryId = subCategoryId;
        }
        public async Task OnGetAsync()
        {
            await _userDataService.CheckCurrentUserAsync(User);
            await LoadCategoriesSideBar();
        }
        private async Task LoadCategoriesSideBar()
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null)
            {
                SideBarOptions.SideBarOptions = categories.Cast<ISideBarOption>().ToList();
            }
        }
        private async Task LoadMainContent()
        {
            if (SubCategoryId == 0)
            {
                var mainContent = await _categoryApiService.GetCategoriesAsync();
                if (mainContent != null)
                {
                    MainContent = mainContent.Cast<IMainContent>().ToList();
                }
            }
            else
            {
                var mainContent = await _subCategoryService.GetSubCategoriesFromParentIdAsync(SubCategoryId);
                if (mainContent != null)
                {
                    MainContent = mainContent.Cast<IMainContent>().ToList();
                }
            }

        }

    }
}
