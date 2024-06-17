using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace ExtremeWeatherBoard.DAL
{
    public class DiscussionThreadService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserDataService _userDataService;
        public DiscussionThreadService(ApplicationDbContext context, UserDataService userDataService)
        {
            _context = context;
            _userDataService = userDataService;
        }
        public async Task<List<DiscussionThread>> GetThreadsAsync(int subCategoryId)
        {
            var threads = await _context.DiscussionThreads
                                           .Where(dt => dt.SubCategoryId == subCategoryId)
                                           .ToListAsync();
            return threads;
        }
        public async Task PostDiscussionThreadAsync(ClaimsPrincipal userPrincipal, int subCategoryId, string title, string text)
        {
            var user = await _userDataService.GetCurrentUserDataAsync(userPrincipal);
            if (user == null)
            {
                Console.WriteLine("No user found");
                Console.ReadKey();
            }
            else
            {
                if (user.GetType() == typeof(UserData))
                {
                    DiscussionThread discussionThread = new DiscussionThread()
                    {
                        DiscussionThreadUserData = user as UserData
                        ,
                        Text = text
                        ,
                        Title = title
                        ,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _context.DiscussionThreads.AddAsync(discussionThread);
                }
            }

        }
        public async Task UpdateDiscussionThreadAsync(ClaimsPrincipal userPrincipal, int discussionThreadId, string text)
        {
            if (userPrincipal.Identity != null)
            {
                var discussionThreadUser = _userDataService.GetCurrentUserDataAsync(userPrincipal);
                var targetDiscussionThread = await _context.DiscussionThreads.FindAsync(discussionThreadId);
                if (targetDiscussionThread != null)
                {
                    if (targetDiscussionThread.DiscussionThreadUserData != null)
                    {
                        if (targetDiscussionThread.DiscussionThreadUserData.Id == discussionThreadUser.Id)
                        {
                            targetDiscussionThread.Text = text;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        public async Task DeleteDiscussionThreadAsync(ClaimsPrincipal userPrincipal, int discussionThreadId)
        {
            bool isAdmin = await _userDataService.CheckIfAdminAsync(userPrincipal);
            if (userPrincipal.Identity != null)
            {
                var discussionThreadUser = _userDataService.GetCurrentUserDataAsync(userPrincipal);
                var targetDiscussionThread = await _context.DiscussionThreads.FindAsync(discussionThreadId);
                if (targetDiscussionThread != null || isAdmin)
                {
                    if (targetDiscussionThread.DiscussionThreadUserData != null || isAdmin)
                    {
                        if (targetDiscussionThread.DiscussionThreadUserData.Id == discussionThreadUser.Id || isAdmin)
                        {
                            _context.DiscussionThreads.Remove(targetDiscussionThread);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }

        }

    }
}
