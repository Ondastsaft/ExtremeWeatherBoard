using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    }
}
