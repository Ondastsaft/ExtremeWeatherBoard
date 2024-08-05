using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.Shared.Views;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExtremeWeatherBoard.Pages.User
{
    public class UserIndexModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        private readonly CommentService _commentService;
        private readonly CategoryApiService _categoryApiService;
        private readonly MessageService _messageService;
        public UserData? CurrentUserData { get; set; }
        public List<DiscussionThread>? DiscussionThreads { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Message>? Messages { get; set; }
        public UserIndexModel(
            UserDataService userDataService
            , UserManager<IdentityUser> userManager
            , SubCategoryService subCategoryService
            , DiscussionThreadService discussionThreadService
            , CommentService commentService
            , CategoryApiService categoryService
            , MessageService messageService
            ) :base(userDataService)
        {
            _userManager = userManager;
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
            _commentService = commentService;
            _categoryApiService = categoryService;
            _messageService = messageService;
        }
        protected override async Task LoadMainContent()
        {
            await LoadDataFromUser();
        }
        protected override async Task LoadSideBar()
        {
            PageSideBarPartialModel = new SideBarPartialViewModel();
            PageSideBarPartialModel.NavigateTo = "/Categories";
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null)
            {
                PageSideBarPartialModel.SideBarOptions = categories.Cast<IContent>().ToList();
            }
        }
        public async Task LoadDataFromUser()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                CurrentUserData = await _userDataService.GetCurrentUserDataAsync(User);
            }
            if (CurrentUserData != null)
            {
                var usersThreads = await _discussionThreadService.GetDiscussionThreadsRelatedTouserAsync(User);
                if (usersThreads != null)
                {
                    DiscussionThreads = usersThreads;
                }
                var usersComments = await _commentService.GetCommentsRelatedToUserAsync(User);
                if (usersComments != null)
                {
                    Comments = usersComments;
                }
                var usersMessages = await _messageService.GetMessagesRelatedToUserAsync(User);
                if (usersMessages != null)
                {
                    Messages = usersMessages;
                }
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
