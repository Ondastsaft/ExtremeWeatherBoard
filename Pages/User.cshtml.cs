using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Services;
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
        private readonly CategoryService _categoryService;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            if (UploadedImage != null)
            {
                if (UploadedImage.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedImage", "The file size exceeds 5 MB.");
                    return Page();
                }
                if (!UploadedImage.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("UploadedImage", "Only image files are allowed.");
                    return Page();
                }
                await _userDataService.PostUserImage(User, UploadedImage);
            }
            return Page();
        }
    }
}
