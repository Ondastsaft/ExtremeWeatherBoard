using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.ContentModel;
using System.Security.Claims;

namespace ExtremeWeatherBoard.Pages
{
    public class MessagesModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserDataService _userDataService;
        private readonly MessageService _messageService;
        public List<Message> MessageThread { get; set; } = new List<Message>();
        public List<Message> Messages { get; set; } = new List<Message>();
        public int MessageId { get; set; }
        public int UserDataId { get; set; }
        public string UserName { get; set; } = String.Empty;
        public int PostMessageReceiverId { get; set; }
        public string MessageReceiverName { get; set; } = String.Empty;
        public string MessageTitle { get; set; } = String.Empty;
        public int PageState { get; set; }
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
        public async Task OnGetAsync(int messageId, int postreceiverId)
        {
            MessageId = messageId;
            await LoadModel(messageId, postreceiverId);
        }
        public async Task OnPostAsync(string title, string text, int receiverId)
        {
            if (await CheckUserState())
            {
                if (title != null && text != null && receiverId != 0)
                {
                    await _messageService.PostMessageAsync(User, receiverId, title, text);
                }
            }
            else
            {
                RedirectToPage("/Index");
            }

        }
        private async Task LoadModel(int messageId, int postReceiverId)
        {
            //check user state and load page content depending on state
            // 1 = view message thread, 2 = reply message, 3 = view all users messages, 4 = post new message
            if (await CheckUserState())
            {
                await SetPageState(messageId, postReceiverId);
                switch (PageState)
                {
                    case 0:
                        RedirectToPage("/Index");
                        break;
                    case 1:
                        await LoadMessageThread(messageId);
                        break;
                    case 2:
                        await LoadPostReplyMessage(messageId, postReceiverId);
                        break;
                    case 3:
                        await LoadAllUsersMessagesAsync();
                        break;
                    case 4:
                        await LoadPostNewMessage(postReceiverId);
                        break;
                }
            }
            else
            {
                RedirectToPage("/Index");
            }
        }
        //Check that a user is logged in else redirect to Index page
        private async Task<bool> CheckUserState()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var UserData = await _userDataService.GetCurrentUserDataAsync(User);
                if (UserData != null)
                {
                    UserDataId = UserData.Id;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check input parameters to determine what state the page is in
        private async Task SetPageState(int messageId, int postReceiverId)
        {
            // if messageid has been passed
            if (messageId != 0)
            {
                var message = await _messageService.GetMessageAsync(messageId, User);
                //check that passed message exists
                if (message != null)
                {
                    //if no postreceiverId has been passed, set page state to 1 to view message thread
                    if (postReceiverId == 0)
                    {
                        PageState = 1;
                        return;
                    }
                    //else set page state 2 for post reply to message
                    else
                    {
                        PageState = 2;
                        return;
                    }
                }
            }
            //if no messageId & no postReceiverId has been passed set page state to 3 to view all messages
            if (messageId == 0 && postReceiverId == 0)
            {
                PageState = 3; return;
            }

            else PageState = 4; return;
        }
        private async Task LoadAllUsersMessagesAsync()
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
            if (message != null && message.Receiver != null)
            {
                PostMessageReceiverId = messageId;
                var messageThread = await _messageService.GetAllMessagesInThreadAsync(User, message);
                if (messageThread != null && messageThread.Count > 0 && messageThread[0].Title != null)
                {
                    MessageTitle = messageThread[0].Title;
                    MessageThread = messageThread.OrderBy(m => m.TimeStamp).ToList();
                }
            }
        }
        private async Task LoadPostNewMessage(int postReceiverId)
        {
            await LoadAllUsersMessagesAsync();
        }
        private async Task LoadPostReplyMessage(int messageId, int postReceiverId)
        {
            var receiverUserData = await _userDataService.GetUserDataAsync(postReceiverId);
            if(receiverUserData != null && receiverUserData.Name != null)
            {
                await LoadMessageThread(messageId);
                PostMessageReceiverId = postReceiverId;
                MessageReceiverName = receiverUserData.Name;
            }
        }
    }
}
