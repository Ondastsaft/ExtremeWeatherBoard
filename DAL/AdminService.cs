using Microsoft.AspNetCore.Identity;

namespace ExtremeWeatherBoard.DAL
{
    public class AdminService
    {
        private readonly UserDataService _userDataService;
        private readonly CategoryAPIService _categoryAPIService;
        private readonly SubcategoryService _subcategoryService;
        private readonly CommentService _commentService;
        private readonly AdminLogService _adminLogService;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminService
            (UserDataService userDataService
            , CategoryAPIService categoryAPIService
            , SubcategoryService subcategoryService
            , CommentService commentService
            , AdminLogService adminLogService
            , UserManager<IdentityUser> userManager
            )
        {
            _userDataService = userDataService;
            _categoryAPIService = categoryAPIService;
            _subcategoryService = subcategoryService;
            _commentService = commentService;
            _adminLogService = adminLogService;
            _userManager = userManager;
        }

    }
}
