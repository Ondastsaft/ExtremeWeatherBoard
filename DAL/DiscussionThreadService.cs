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
        public async Task<List<DiscussionThread>> GetDiscussionThreadsAsync(int subCategoryId)
        {
            var threads = await _context.DiscussionThreads
                                           .Include(dt => dt.DiscussionThreadUserData)
                                           .Where(dt => dt.SubCategoryId == subCategoryId)
                                           .ToListAsync();
            return threads;
        }
        public async Task<DiscussionThread?> GetDiscussionThreadAsync(int discussionThreadId)
        {
            var discussionThread = await _context.DiscussionThreads
                                                .Include(dt => dt.DiscussionThreadUserData)
                                                .FirstOrDefaultAsync(dt => dt.Id == discussionThreadId);
            return discussionThread;
        }
        public async Task PostDiscussionThreadAsync(ClaimsPrincipal claimsPrincipal, int subCategoryId, string title, string text)
        {
            var user = await _userDataService.GetCurrentUserDataAsync(claimsPrincipal);
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
            if (user.Id == 0 || subCategory == null)
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
                        DiscussionThreadUserData = user as UserData,
                        SubCategory = subCategory,
                        Text = text,
                        Title = title,
                        IsReported = false,
                        TimeStamp = DateTime.UtcNow
                    };
                    await _context.DiscussionThreads.AddAsync(discussionThread);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task UpdateDiscussionThreadAsync(ClaimsPrincipal claimsPrincipal, int discussionThreadId, string text)
        {
            if (claimsPrincipal.Identity != null)
            {
                var discussionThreadUser = _userDataService.GetCurrentUserDataAsync(claimsPrincipal);
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
        public async Task DeleteDiscussionThreadAsync(ClaimsPrincipal claimsPrincipal, int discussionThreadId)
        {
            bool isAdmin = await _userDataService.CheckIfAdminAsync(claimsPrincipal);
            if (claimsPrincipal.Identity != null)
            {
                var discussionThreadUser = _userDataService.GetCurrentUserDataAsync(claimsPrincipal);
                var targetDiscussionThread = await _context.DiscussionThreads.FirstOrDefaultAsync(d => d.Id == discussionThreadId);

                if (targetDiscussionThread?.DiscussionThreadUserData?.Id == discussionThreadUser.Id)
                {
                    _context.DiscussionThreads.Remove(targetDiscussionThread);
                    await _context.SaveChangesAsync();
                }
                if (isAdmin && targetDiscussionThread != null)
                {
                    _context.DiscussionThreads.Remove(targetDiscussionThread);
                    await _context.SaveChangesAsync();
                }
            }

        }
        public async Task ReportDiscussionThread(int discussionThreadId)
        {
            var discussionThread = _context.DiscussionThreads.FirstOrDefault(dt => dt.Id == discussionThreadId);
            if (discussionThread != null)
            {
                discussionThread.IsReported = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<DiscussionThread>?> GetDiscussionThreadsRelatedTouserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userDataService.GetCurrentUserDataAsync(claimsPrincipal);
            if (user != null)
            {
                var threads = await _context.DiscussionThreads.Where(dt => dt.DiscussionThreadUserDataId == user.Id).ToListAsync();
                return threads;
            }
            return null;
        }

    }
}
