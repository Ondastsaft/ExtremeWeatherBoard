using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Pages
{
    public class PostMessageModel : BasePageModel
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserDataService _userDataService;
        private readonly MessageService _messageService;
        public int SenderId { get; set; }
        public string Receivername { get; set; } = String.Empty;
        [BindProperty]
        public string Text { get; set; } = String.Empty;
        [BindProperty]
        public string Title { get; set; } = String.Empty;
        [BindProperty]

        public int ReceiverId { get; set; }
        public PostMessageModel(
        UserManager<IdentityUser> userManager,
        MessageService messageService,
        UserDataService userDataService,
        CategoryApiService categoryApiService
        )
        {
            _userManager = userManager;
            _messageService = messageService;
            _userDataService = userDataService;
            _categoryApiService = categoryApiService;
        }
        public async Task OnGetAsync(int messageReceiverId, int messageId)
        {
            if (messageReceiverId != 0)
            {

                if (messageId != 0)
                {
                    await LoadMainContentviewModel(messageReceiverId, messageId);
                }
                else await LoadNewMessageDataAsync(messageReceiverId);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (CheckPostedMessage())
            {
                    await _messageService.PostMessageAsync(User, ReceiverId, Title, Text);                
            }
            return RedirectToPage("/Messages");
        }

        private bool CheckPostedMessage()
        {
            if (
                ReceiverId != 0 &&
                Text != String.Empty &&
                Title != String.Empty)
            {
                return true;
            }
            else return false;
        }
        private async Task LoadMainContentviewModel(int messageReceiverId, int messageId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var senderUserData = await _userDataService.GetCurrentUserDataAsync(User);
                var receiverUserData = await _userDataService.GetUserDataAsync(messageReceiverId);
                if (receiverUserData != null && receiverUserData.Name != null)
                {
                    Receivername = receiverUserData.Name;
                    ReceiverId = receiverUserData.Id;
                }
                if (messageId != 0)
                {
                    var message = await _messageService.GetMessageAsync(messageId, User);
                    if (message != null)
                    {
                        await LoadMessageThreadAsync(messageId);
                    }
                }
            }
        }
        private async Task LoadMessageThreadAsync(int messageId)
        {
            var message = await _messageService.GetMessageAsync(messageId, User);
            if (message != null)
            {
                MainContent = new MainContentViewModel();
                var mainContent = await _messageService.GetAllMessagesInThreadAsync(User, message);
                if (mainContent != null)
                {
                    mainContent.OrderBy(m => m.TimeStamp).ToList();
                    MainContent.MainContentList = mainContent;
                }
            }
        }
        private async Task LoadNewMessageDataAsync(int messageReceiverId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var receiverUserData = await _userDataService.GetUserDataAsync(messageReceiverId);
                if (receiverUserData != null && receiverUserData.Name != null)
                {
                    Receivername = receiverUserData.Name;
                    ReceiverId = receiverUserData.Id;
                }
            }
        }
        private async Task LoadSideBarModel()
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/Categories";
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null)
            {
                SideBarOptions.SideBarOptions = categories.Cast<ISideBarOption>().ToList();
            }
        }
    }
}
