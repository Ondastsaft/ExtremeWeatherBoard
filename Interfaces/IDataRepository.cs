using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Interfaces
{
    public interface IDataRepository
    {
        Task<bool> PopulateUserDatasAsync();
        Task<bool> PopulateAdminUserDatasAsync();
        Task<bool> PopulateCategoriesAsync();
        Task<bool> PopulateSubCategoriesAsync();
        Task<bool> PopulateDiscussionThreadsAsync();
        Task<bool> PopulateCommentsAsync();
        Task<bool> PopulateMessagesAsync();
        Task<bool> PopulateAdminLogsAsync();
        Task<bool> EditUserDataAsync(UserData userData);
        Task<bool> EditAdminUserAsync(AdminUserData AdminuserData);
        Task<bool> EditCategoryAsync(Category category);
        Task<bool> EditSubCategoryAsync(SubCategory subCategory);
        Task<bool> EditDiscussionThreadAsync(DiscussionThread thread);
        Task<bool> AddUserDataAsync(UserData userData);
        Task<bool> AddAdminUserDataAsync(AdminUserData adminUserData);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> AddSubCategoryAsync(SubCategory subCategory);
        Task<bool> AddDiscussionThreadAsync(DiscussionThread thread);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> AddMessageAsync(Message message);
        Task<bool> AddAdminLogAsync(AdminLog adminLog);
        Task<bool> DeleteUserDataAsync(UserData userData);
        Task<bool> DeleteAdminUserAsync(AdminUserData AdminuserData);
        Task<bool> DeleteCategoryAsync(Category category);
        Task<bool> DeleteSubCategoryAsync(SubCategory subCategory);
        Task<bool> DeleteDiscussionThreadAsync(DiscussionThread thread);
        Task<bool> DeleteCommentAsync(Comment comment);
        Task<bool> DeleteMessageAsync(Message message);
    }
}
