using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class AdminModel : PageModel
    {
        private readonly AdminService _adminService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CategoryApiService _categoryApiService;
        private readonly UserDataService _userDataService;
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public int SubCategoryId { get; set; }
        [BindProperty]
        public int UserDataId { get; set; }
        [BindProperty]
        public int ParentCategoryId { get; set; }
        [BindProperty]
        public string CategoryTitle { get; set; } = String.Empty;
        [BindProperty]
        public string SubCategoryTitle { get; set; } = String.Empty;
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
        public async Task OnGetAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await LoadModelAsync();
            }
        }
        public async Task<IActionResult> OnPostCreateCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.PostCategoryAsync(CategoryTitle, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostCreateSubCategoryAsync()
        {             if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.PostSubCategoryAsync(SubCategoryTitle, ParentCategoryId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAssignAdminRoleAsync()
            {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.AssignAdminRoleAsync(UserDataId);
                return RedirectToPage();
            }
            return Page();
        }



        private async Task LoadModelAsync()
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

