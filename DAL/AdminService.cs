using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtremeWeatherBoard.DAL
{
    public class AdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserDataService _userDataService;
        private readonly CategoryApiService _categoryAPIService;
        private readonly SubCategoryService _subcategoryService;
        private readonly CommentService _commentService;
        private readonly AdminLogService _adminLogService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService
            (
            ApplicationDbContext context
            , UserDataService userDataService
            , CategoryApiService categoryAPIService
            , SubCategoryService subcategoryService
            , CommentService commentService
            , AdminLogService adminLogService
            , UserManager<IdentityUser> userManager
            , RoleManager<IdentityRole> roleManager

            )
        {
            _context = context;
            _userDataService = userDataService;
            _categoryAPIService = categoryAPIService;
            _subcategoryService = subcategoryService;
            _commentService = commentService;
            _adminLogService = adminLogService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task PostCategoryAsync(string title, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.IsInRole("Admin"))
            {
                var adminUserData = _context.AdminUserDatas.FirstOrDefault(a => a.UserId == _userManager.GetUserId(claimsPrincipal));
                var newCategory = new Category() { Title = title, CreatorAdminUserData = adminUserData, TimeStamp = DateTime.UtcNow };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
            }
        }
        public async Task EditCategoryAsync(int categoryId, string title, ClaimsPrincipal claimsPrincipal)
        {
            if (categoryId > 0 && claimsPrincipal.IsInRole("Admin"))
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category != null)
                {
                    category.Title = title;
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteCategoryAsync(int categoryId, ClaimsPrincipal claimsPrincipal)
        {
            if (categoryId > 0 && claimsPrincipal.IsInRole("Admin"))
            {
                await DeleteAllSubCategoriesFromCategory(categoryId);
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        await _context.SaveChangesAsync();
                    }                
            }
        }
        public async Task PostSubCategoryAsync(string title, int parentCategoryId, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(claimsPrincipal);
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userId);
                if (adminUserData != null)
                {
                    var parentcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == parentCategoryId);
                    if (parentcategory != null)
                    {
                        var subcategory = new SubCategory()
                        {
                            Title = title,
                            ParentCategory = parentcategory,
                            CreatorAdminUserData = adminUserData,
                            TimeStamp = DateTime.UtcNow
                        };
                        _context.SubCategories.Add(subcategory);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task EditSubCategoryAsync(int subCategoryId, string title, int parentCategoryId, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(claimsPrincipal);
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userId);
                if (adminUserData != null)
                {
                    var subCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == subCategoryId);
                    if (subCategory != null)
                    {
                        subCategory.Title = title;
                        var parentCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == parentCategoryId);
                        if (parentCategory != null)
                        {
                            subCategory.ParentCategory = parentCategory;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }

        }
        public async Task DeleteSubCategoryAsync(int subCategoryId, ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.IsInRole("Admin"))
            {

                    var subCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == subCategoryId);
                    if (subCategory != null)
                    {
                        await DeleteAllThreadsFromSubCategory(subCategoryId);
                        _context.SubCategories.Remove(subCategory);
                        await _context.SaveChangesAsync();
                    }
                
            }
        }
        public async Task AssignAdminRoleAsync(int userId, ClaimsPrincipal claimsPrincipal)
        {
            await AssignAdminRolePrivateAsync(userId, claimsPrincipal);
        }
        private async Task AssignAdminRolePrivateAsync(int userDataId, ClaimsPrincipal claimsPrincipal)
        {
            var userData = await _userDataService.GetUserDataAsync(userDataId);
            if (claimsPrincipal.IsInRole("Admin") && userData.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(userData.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    await PostAdminUserDataAsync(user.Id);
                }
            }
        }
        public async Task RemoveAdminRoleAsync(int userId, ClaimsPrincipal claimsPrincipal)
        {
            await RemoveAdminRoleAsync(userId, claimsPrincipal);
        }
        private async Task RemoveAdminRolePrivateAsync(int userDataId, ClaimsPrincipal claimsPrincipal)
        {
            var userData = await _userDataService.GetUserDataAsync(userDataId);
            if (claimsPrincipal.IsInRole("Admin") && userData.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(userData.UserId);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                }
            }
        }
        public async Task RemoveUserAsync(int userId, ClaimsPrincipal claimsPrincipal)
        {
            var userData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.Id == userId);
            if (claimsPrincipal.IsInRole("Admin") && userData?.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(userData.UserId);

                // Create a new IdentityUser to assign to the UserData
                var newUser = new IdentityUser
                {
                    UserName = $"deleted_user_{Guid.NewGuid()}",
                    Email = $"deleted_user_{Guid.NewGuid()}@example.com"
                };
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    // Assign the new IdentityUser to the UserData
                    userData.UserId = newUser.Id;
                    userData.Name = "Deleted User";
                    userData.ImageURL = "/images/defaultuser.jpg";
                    await _context.SaveChangesAsync();

                    // Delete the old IdentityUser
                    if (user != null)
                    {
                        await _userManager.DeleteAsync(user);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        private async Task InitAdminRolePrivateAsync(int userId)
        {

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var userData = await _userDataService.GetUserDataAsync(userId);
            if (userData?.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(userData.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    await PostAdminUserDataAsync(user.Id);
                }
            }

        }
        public async Task PostAdminUserDataAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var adminUserData = new AdminUserData() { UserId = userId };
            _context.AdminUserDatas.Add(adminUserData);
            await _context.SaveChangesAsync();
        }
        private async Task DeleteAllSubCategoriesFromCategory(int categoryId)
        {
            var subCategories = await _context.SubCategories.Where(s => s.ParentCategoryId == categoryId).ToListAsync();
            if (subCategories != null)
            {
                foreach (var subCategory in subCategories)
                {
                    await DeleteAllThreadsFromSubCategory(subCategory.Id);
                    _context.SubCategories.Remove(subCategory);
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task DeleteAllThreadsFromSubCategory(int subCategoryId)
        {
            var threads = await _context.DiscussionThreads.Where(t => t.SubCategoryId == subCategoryId).ToListAsync();
            if (threads != null)
            {
                foreach (var thread in threads)
                {
                    await DeleteAllCommentsFromThread(thread.Id);
                    _context.DiscussionThreads.Remove(thread);
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task DeleteAllCommentsFromThread(int threadId)
        {
            var comments = await _context.Comments.Where(c => c.ParentDiscussionThreadId == threadId).ToListAsync();
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    await _commentService.DeleteCommentAsync(comment.Id);
                }
            }
        }
    }
}
