using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtremeWeatherBoard.DAL
{
    public class UserDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserDataService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<UserData> GetCurrentUserDataAsync(ClaimsPrincipal claimsPrincipal)
        {         
                if (claimsPrincipal.Identity?.IsAuthenticated != null)
                {
                var userId = _userManager.GetUserId(claimsPrincipal);
                if (userId != null)
                    {
                        var currentUserData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.UserId == userId);
                        if (currentUserData != null)
                        {
                            return currentUserData;
                        }
                    }
                }            
                return GuestUserService.GuestUserData;
        }

        public async Task<List<UserData>> GetAllUserDataAsync()
        {
            return await _context.UserDatas.ToListAsync();
        }
        public async Task<UserData> GetUserDataAsync(int id)
        {
            var foundUserData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.Id == id);
            if (foundUserData != null)
            {
                return foundUserData;
            }
            return GuestUserService.GuestUserData;
        }
        public async Task<bool> CheckUserDataAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity != null)
            {
                if (claimsPrincipal.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(claimsPrincipal);
                    if (await _context.UserDatas.AnyAsync(u => u.UserId == userId))
                    {
                        return true;
                    }
                    else
                    {
                        var userData = new UserData() { UserId = userId };
                        await _context.UserDatas.AddAsync(userData);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<bool> CheckIfAdminAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity?.Name != null)
            {
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == claimsPrincipal.Identity.Name);
                if (adminUserData != null)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<UserData> PostUserDataAsync(string userId, string name)
        {
            var userData = new UserData() { UserId = userId, Name = name, ImageURL = "/images/defaultuser.jpg" };
            _context.UserDatas.Add(userData);
            await _context.SaveChangesAsync();
            return userData;
        }
        public async Task<bool> CheckCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(claimsPrincipal);
                if (_context.UserDatas.Any(ud => ud.UserId == userId))
                {
                    return true;
                }
                UserData userData = new UserData() { UserId = userId };
                await _context.AddRangeAsync(userData);
                await _context.SaveChangesAsync();
                if (_context.UserDatas.Any(ud => ud.UserId == userId))
                {
                    return true;
                }
            }
            return false;
        }
        public async Task UpdateUserDataAsync(UserData userData)
        {
            _context.UserDatas.Update(userData);
            await _context.SaveChangesAsync();
        }
        public async Task PostUserImage(ClaimsPrincipal claimsPrincipal,IFormFile file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var userid = _userManager.GetUserId(claimsPrincipal);
            if (userid != null)
            {
                var userData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.UserId == userid);
                if (userData != null)
                {
                    userData.ImageURL = $"/Images/{file.FileName}";
                    await UpdateUserDataAsync(userData);
                }
            }
        }
    }
}
