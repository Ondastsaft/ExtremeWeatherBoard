namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.Interfaces;
    using ExtremeWeatherBoard.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public List<UserData>? Users { get; private set; }
        public List<AdminUserData>? AdminUsers;
        public List<Category>? Categories;
        public List<SubCategory>? SubCategories;
        public List<DiscussionThread>? DiscussionThreads;
        public List<Comment>? Comments;
        public List<Message>? Messages;
        public List<AdminLog>? AdminLog;

        public DataRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> PopulateUserDatasAsync()
        {
            Users = await _context.UserDatas.ToListAsync();
            if (Users.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateAdminUserDatasAsync()
        {
            AdminUsers = await _context.AdminUserDatas.ToListAsync();
            if (AdminUsers.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateCategoriesAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            if (Categories.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateSubCategoriesAsync()
        {
            SubCategories = await _context.SubCategories.ToListAsync();
            if (SubCategories.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateDiscussionThreadsAsync()
        {
            DiscussionThreads = await _context.DiscussionThreads.ToListAsync();
            if (DiscussionThreads.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateCommentsAsync()
        {
            Comments = await _context.Comments.ToListAsync();
            if (Comments.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateMessagesAsync()
        {
            Messages = await _context.Messages.ToListAsync();
            if (Messages.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> PopulateAdminLogsAsync()
        {
            AdminLog = await _context.AdminLogs.ToListAsync();
            if (AdminLog.Count == 0) { return false; }
            return true;
        }
        public async Task<bool> EditUserDataAsync(UserData userData)
        {
            _context.UserDatas.Update(userData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditAdminUserAsync(AdminUserData AdminuserData)
        {
            _context.AdminUserDatas.Update(AdminuserData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditDiscussionThreadAsync(DiscussionThread discussionThread)
        {
            _context.DiscussionThreads.Update(discussionThread);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddUserDataAsync(UserData userData)
        {
            _context.UserDatas.Add(userData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAdminUserDataAsync(AdminUserData adminUserData)
        {
            _context.AdminUserDatas.Add(adminUserData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddDiscussionThreadAsync(DiscussionThread discussionThread)
        {
            _context.DiscussionThreads.Add(discussionThread);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAdminLogAsync(AdminLog adminLog)
        {
            _context.AdminLogs.Add(adminLog);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserDataAsync(UserData userData)
        {
            _context.UserDatas.Remove(userData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAdminUserAsync(AdminUserData adminUserData)
        {
            _context.AdminUserDatas.Remove(adminUserData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDiscussionThreadAsync(DiscussionThread discussionThread)
        {
            _context.DiscussionThreads.Remove(discussionThread);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMessageAsync(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
