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
        public string NewCategoryTitle { get; set; } = String.Empty;
        [BindProperty]
        public int EditCategoryId { get; set; }
        [BindProperty]        
        public string EditCategoryTitle { get; set; } = String.Empty;
        [BindProperty]
        public int DeleteCategoryId { get; set; }
        [BindProperty]        
        public string NewSubCategoryTitle { get; set; } = String.Empty;
        [BindProperty]
        public int NewSubCategoryParentCategoryId { get; set; }
        [BindProperty]
        public int EditSubCategoryId { get; set; }
        [BindProperty]
        public int EditSubCategoryParentCategoryId { get; set; }
        [BindProperty]
        public string EditSubCategoryTitle { get; set; } = String.Empty;
        [BindProperty]
        public int DeleteSubCategoryId { get; set; }
        [BindProperty]
        public int AssignAdminUserDataId { get; set; }
        [BindProperty]
        public int RemoveAdminUserDataId { get; set; }
        [BindProperty]
        public int RemoveUserDataId { get; set; }
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
        public async Task<IActionResult> OnPostCreateNewCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.PostCategoryAsync(NewCategoryTitle, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                var category = await _categoryApiService.GetCategoryAsync(EditCategoryId);
                if (category != null)
                {
                    await _adminService.EditCategoryAsync(EditCategoryId, EditCategoryTitle, User);
                }
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.DeleteCategoryAsync(DeleteCategoryId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostCreateSubCategoryAsync()
        {             
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.PostSubCategoryAsync(NewSubCategoryTitle, NewSubCategoryParentCategoryId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditSubCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.EditSubCategoryAsync(EditSubCategoryId, EditSubCategoryTitle, EditSubCategoryParentCategoryId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteSubCategoryAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.DeleteSubCategoryAsync(DeleteSubCategoryId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAssignAdminRoleAsync()
            {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.AssignAdminRoleAsync(AssignAdminUserDataId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostRemoveAdminRoleAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.RemoveAdminRoleAsync(RemoveAdminUserDataId, User);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostRemoveUserAsync()
        {
            if (User.Identity != null && User.IsInRole("Admin"))
            {
                await _adminService.RemoveUserAsync(RemoveUserDataId, User);
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

