using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using ExtremeWeatherBoard.DTO;
using System.Globalization;

namespace ExtremeWeatherBoard.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SubCategoryService _subCategoryService;
        public bool SubCategoryState = false;

        public List<MainNavObjectDTO> MainNavObjects { get; set; } = new();
        public IndexModel(
            CategoryApiService categoryApiService
            , UserDataService userDataService
            , UserManager<IdentityUser> usermanager
            , SubCategoryService subCategoryService
            )
        {
            _categoryApiService = categoryApiService;
            _userDataService = userDataService;
            _userManager = usermanager;
            _subCategoryService = subCategoryService;
        }
        public async Task OnGetAsync(int categoryId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                await _userDataService.CheckCurrentUserAsync(User);
            if (categoryId == 0)
            {
                MainNavObjects = await LoadCategoryNavObjectsAsync();
            }
            else
            {
                MainNavObjects = await LoadSubCategoryNavObjectsAsync(categoryId);
                SubCategoryState = true;
            }
        }

        private async Task<List<MainNavObjectDTO>> LoadSubCategoryNavObjectsAsync(int categoryId)
        {
            var mainNavObjectList = new List<MainNavObjectDTO>();
            var navObjects = await _subCategoryService.GetSubCategoriesWithTopicsAsync(categoryId);

            foreach (var subCategory in navObjects)
            {
                var objecDto = new MainNavObjectDTO();
                objecDto.Id = subCategory.Id;
                objecDto.Title = subCategory.Title ?? "NavObject title not found";
                if (subCategory.Threads != null)
                {
                    foreach (var navContent in subCategory.Threads)
                    {
                        var navContentDTO = new MainNavContentDTO();
                        navContentDTO.Id = navContent.Id;
                        navContentDTO.Title = navContent.Title ?? "NavContent title not found";
                        objecDto.NavContentDTOs.Add(navContentDTO);
                    }
                }
                mainNavObjectList.Add(objecDto);
            }
            var sortedMainNavObjects = new List<MainNavObjectDTO>();
            for (int i = 0; i < mainNavObjectList.Count; i++)
            {
                sortedMainNavObjects.Add(mainNavObjectList.Last());
                if(i == 4)
                {
                    break;
                }
            }
            return sortedMainNavObjects;
        }
        private async Task<List<MainNavObjectDTO>> LoadCategoryNavObjectsAsync()
        {
            var mainNavObjectList = new List<MainNavObjectDTO>();
            var navObjects = await _categoryApiService.GetCategoriesAsync();

            foreach (var category in navObjects)
            {
                var objecDto = new MainNavObjectDTO();
                objecDto.Id = category.Id;
                objecDto.Title = category.Title ?? "NavObject title not found";
                if (category.SubCategories != null)
                {
                    foreach (var navContent in category.SubCategories)
                    {
                        var navContentDTO = new MainNavContentDTO();
                        navContentDTO.Id = navContent.Id;
                        navContentDTO.Title = navContent.Title ?? "NavContent title not found";
                        objecDto.NavContentDTOs.Add(navContentDTO);
                    }
                }
                mainNavObjectList.Add(objecDto);
            }
            return mainNavObjectList;
        }


    }
}
