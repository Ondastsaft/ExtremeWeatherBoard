using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    using ExtremeWeatherBoard.DAL;
    using ExtremeWeatherBoard.Models;
    using Microsoft.AspNetCore.Identity;

    public class AdminModel : PageModel
    {
        private readonly AdminService _adminService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CategoryApiService _categoryApiService;
        private readonly UserDataService _userDataService;
        public List<Category> Categories { get; set; } = new();
        public List<SubCategory> SubCategories { get; set; } = new();
        public List<UserData> UserDatas { get; set; } = new();
        public AdminModel(
            AdminService adminService,
            UserManager<IdentityUser> userManager,
            CategoryApiService categoryApiService,
            UserDataService userDataService
            )
        {
            _adminService = adminService;
            _userManager = userManager;
            _categoryApiService = categoryApiService;
            _userDataService = userDataService;
        }
        public void OnGet()
        {
            
        }
        private async Task LoadModel()
        {
            var categories = await _categoryApiService.GetCategoriesWithSubCategoriesAsync();
            if (categories != null)
            {
                Categories = categories;
                foreach (var category in categories)
                {
                    if (category.SubCategories != null)
                    {
                        foreach (var subCategory in category.SubCategories)
                        {
                            SubCategories.Add(subCategory);
                        }
                    }
                }
            }
            var userDatas = await _userDataService.GetAllUserDataAsync();
            if (userDatas != null)
            {
                UserDatas = userDatas;
            }
        }
    }
}
