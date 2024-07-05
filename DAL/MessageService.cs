using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtremeWeatherBoard.DAL
{
    public class MessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;
        public MessageService(ApplicationDbContext context, UserDataService userDataService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userDataService = userDataService;
            _userManager = userManager;
        }
        public async Task<Message?> GetMessageAsync(int messageId, ClaimsPrincipal userPrincipal)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            var userData = await _userDataService.GetCurrentUserDataAsync(userPrincipal);
            if (message != null && userData != null)
            {
                if (message.SenderId == userData.Id || message.ReceiverId == userData.Id)
                {
                    return message;
                }
            }
            return null;
        }
        public async Task<List<Message>> GetFirstMessageForEachtThreadAsync(ClaimsPrincipal userPrincipal)
        {
            List<Message> messageThreads = new List<Message>();
            var usersMessages = await GetMessagesRelatedToUserAsync(userPrincipal);
            if (usersMessages != null)
            {
                foreach (var message in usersMessages) 
                {
                    bool listed = false;
                    foreach (var foundMessage in messageThreads)
                    {
                        if (foundMessage.Title == message.Title)
                            listed = true;
                    }
                    if (!listed)
                    {
                        messageThreads.Add(message);
                    }
                }
            }
            return messageThreads;
        }
        public async Task<List<Message>> GetAllMessagesInThreadAsync(ClaimsPrincipal userPrincipal,Message originalMessage)
        {
            List<Message> messageThread = new List<Message>();
            var usersMessages = await GetMessagesRelatedToUserAsync(userPrincipal);
            if (usersMessages != null)
            {
                foreach (var message in usersMessages)
                {
                    if (originalMessage.Title == message.Title)
                    { 
                        messageThread.Add(message);
                    }
                }
            }
            return messageThread;
        }
        public async Task PostMessageAsync(ClaimsPrincipal userPrincipal, int receiverUserDataId, string title, string text)
        {
            var senderUserData = await _userDataService.GetCurrentUserDataAsync(userPrincipal);
            if (senderUserData != null && senderUserData.Id !=0) 
            {
                UserData receiverUserData = await _userDataService.GetUserDataAsync(receiverUserDataId);
                if (receiverUserData != null) 
                { 
                Message sentMessage = new Message() { Receiver = receiverUserData, 
                    Sender = senderUserData,
                    Title = title,
                    Text = text,
                    TimeStamp = DateTime.UtcNow
                };
                    _context.Messages.Add(sentMessage);
                    _context.SaveChanges();
                }
            }
        }
        public async Task DeleteMessageAsync(int messageId)
        {
          var messsageToBeDeleted = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (messsageToBeDeleted != null)
            {
                _context.Messages.Remove(messsageToBeDeleted);
            }
        }
        internal async Task<List<Message>> GetMessagesRelatedToUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userData = await _userDataService.GetCurrentUserDataAsync(userPrincipal);

            List<Message> messages = new List<Message>();
                    var sentMessages = await _context.Messages.Include(m => m.Sender).Include(m => m.Receiver).Where(m => m.SenderId == userData.Id).ToListAsync();
                    foreach (var sentMessage in sentMessages)
                    {
                        messages.Add(sentMessage);
                    }
                    var receivedMessages = await _context.Messages.Where(m => m.ReceiverId == userData.Id).ToListAsync();
                    foreach (var receivedMessage in receivedMessages)
                    {
                        messages.Add(receivedMessage);
                    }                            
            return messages;
        }
     
        
    }
}
