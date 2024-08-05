using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ExtremeWeatherBoard.Pages.Messages
{
    public class MessagesIndexModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MessageService _messageService;
        private readonly CategoryApiService _categoryApiService;
        public List<Message>? MessageThread { get; set; }
        public List<Message>? Messages { get; set; }
        public int UserDataId { get; set; }
        public MessagesIndexModel(
            UserManager<IdentityUser> userManager,
            MessageService messageService,
            UserDataService userDataService,
            CategoryApiService categoryApiService
            ) :  base(userDataService)
        {
            _userManager = userManager;
            _messageService = messageService;
            _categoryApiService = categoryApiService;
        }
        public override async Task OnGetAsync(int mainContentId, int sideBarContentId)
        {
            MainContentId = mainContentId;
            await LoadSideBar();
            await LoadMainContent();            
        }
        public async Task OnPostAsync(string title, string text, int receiverId)
        {
            if (title != null && text != null && receiverId != 0)
            {
                await _messageService.PostMessageAsync(User, receiverId, title, text);
            }
        }
        protected override async Task LoadSideBar()
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            if(categories != null)
            {
                PageSideBarPartialModel.SideBarOptions = categories.Cast<IContent>()
                .ToList();

            }

        }
        protected override async Task LoadMainContent()
        {
            await LoadModel(MainContentId);
        }
        private async Task LoadModel(int messageId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var UserData = await _userDataService.GetCurrentUserDataAsync(User);
                if (UserData != null)
                {
                    UserDataId = UserData.Id;
                }
                if (messageId != 0)
                {
                    var message = await _messageService.GetMessageAsync(messageId, User);
                    if (message != null)
                    {
                        await LoadMessageThread(messageId);
                    }
                }
                if (messageId == 0)
                {
                    await LoadMessagesAsync();

                }
            }
            else
            {
                RedirectToPage("/Index");
            }
        }
        private async Task LoadMessagesAsync()
        {
            var messages = await _messageService.GetFirstMessageForEachtThreadAsync(User);
            if (messages != null)
            {
                messages.OrderBy(m => m.TimeStamp).ToList();
                Messages = messages;
            }
        }
        private async Task LoadMessageThread(int messageId)
        {
            var message = await _messageService.GetMessageAsync(messageId, User);
            if (message != null)
            {
                MessageThread = await _messageService.GetAllMessagesInThreadAsync(User, message);
                MessageThread.OrderBy(m => m.TimeStamp).ToList();
            }
        }
    }
}
