using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ExtremeWeatherBoard.Pages
{
    public class MessagesModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserDataService _userDataService;
        private readonly MessageService _messageService;
        public List<Message>? MessageThread { get; set; }
        public List<Message>? Messages { get; set; }
        public int UserDataId { get; set; }
        public MessagesModel(
            UserManager<IdentityUser> userManager,
            MessageService messageService,
            UserDataService userDataService
            )
        {
            _userManager = userManager;
            _messageService = messageService;
            _userDataService = userDataService;
        }
        public async Task OnGetAsync(int messageId, int receiverId)
        {
            await LoadModel(messageId);
        }
        public async Task OnPostAsync(string title, string text, int receiverId)
        {
            if (title != null && text != null && receiverId != 0)
            {
                await _messageService.PostMessageAsync(User, receiverId, title, text);
            }
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
