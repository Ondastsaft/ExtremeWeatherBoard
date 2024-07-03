using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ExtremeWeatherBoard.Pages
{
    public class UserModel : BasePageModel
    {
        private readonly UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        private readonly CommentService _commentService;
        private readonly CategoryApiService _categoryApiService;
        public UserData? CurrentUserData { get; set; }
        public UserModel(
            UserDataService userDataService
            , UserManager<IdentityUser> userManager
            , SubCategoryService subCategoryService
            , DiscussionThreadService discussionThreadService
            , CommentService commentService
            , CategoryApiService categoryService
            )
        {
            _userDataService = userDataService;
            _userManager = userManager;
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
            _commentService = commentService;
            _categoryApiService = categoryService;
        }
        public async Task OnGetAsync()
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/Categories";
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null)
            {
                SideBarOptions.SideBarOptions = categories.Cast<ISideBarOption>().ToList();
            }
            if (User.Identity?.IsAuthenticated == true)
            {
                CurrentUserData = await _userDataService.GetCurrentUserDataAsync(User);
            }
        }
        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if (UploadedImage != null)
                {
                    if (CheckImageType(UploadedImage))
                    {
                        await _userDataService.PostUserImage(User, UploadedImage);
                    }
                    else
                    {
                        ModelState.AddModelError("UploadedImage", "The file size exceeds 5 MB or is not an image");
                        return Page();
                    }
                } 
            }
            return Page();
        }
        public bool CheckImageType(IFormFile image)
        {
            if (image != null)
            {              
                if (image.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedImage", "The file size exceeds 5 MB.");
                    return false;
                }
                if (image.ContentType.StartsWith("image/"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
