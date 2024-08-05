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
    using ExtremeWeatherBoard.Pages.Shared.Views;
    using Microsoft.AspNetCore.Identity;


    public class IndexModel : BasePageModel
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SubCategoryService _subCategoryService;
        private readonly MockDataGenerator _dataGenerator;
        private static readonly Uri BaseAdress = new ("https://localhost:44311/api/");
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public IndexModel(  
            CategoryApiService categoryApiService
            ,UserDataService userDataService 
            ,UserManager<IdentityUser> usermanager,
            SubCategoryService subCategoryService
            , MockDataGenerator dataGenerator
            ) : base(userDataService)
        {
            _categoryApiService = categoryApiService;
            _userManager = usermanager;
            _dataGenerator = dataGenerator;
            _subCategoryService = subCategoryService;
        }
        protected override async Task LoadSideBar()
        {
            var categories = await _categoryApiService.GetCategoriesWithSubCategoriesAsync();
            if (categories != null)
            {
                PageSideBarPartialModel.SideBarOptions = categories.Cast<IContent>().ToList();
                PageSideBarPartialModel.NavigateTo = "/Topic";
            }
            else PageSideBarPartialModel.NavigateTo = "/Index";
        }
        protected override async Task LoadMainContent()
        {
            if (CategoryId == 0)
            {
                PageMainContentPartialModel.NavigateTo = "/Index";
                var mainContent = await _categoryApiService.GetCategoriesAsync();
                if (mainContent != null)
                {
                    PageMainContentPartialModel.MainContentList = mainContent.Cast<IContent>().ToList();
                }
            }
            else
            {
                PageMainContentPartialModel.NavigateTo = "/Topic";
                var mainContent = await _subCategoryService.GetSubCategoriesFromParentIdAsync(CategoryId);
                if (mainContent != null)
                {
                    PageMainContentPartialModel.MainContentList = mainContent.Cast<IContent>().ToList();
                }
            }

        }

    }
}
