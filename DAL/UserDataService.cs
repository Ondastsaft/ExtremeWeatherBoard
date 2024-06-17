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

        public async Task<UserData> GetCurrentUserDataAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity != null)
            {
                if (userPrincipal.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(userPrincipal);
                    if (userId != null)
                    {
                        var currentUserData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.UserId == userId);
                        if (currentUserData != null)
                        {
                            return currentUserData;
                        }
                    }
                }
            }
                return GuestUserService.GuestUserData;
        }
        public async Task<IUser?>? GetUserDataAsync(int id)
        {
            var foundUserData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.Id == id);
            return foundUserData;
        }
        public async Task<bool> CheckUserDataAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity.IsAuthenticated)
            {
                var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await _context.UserDatas.AnyAsync(u => u.UserId == userId))
                {
                    return true;
                }
                if (await _context.AdminUserDatas.AnyAsync(a => a.UserId == userId))
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
            return false;
        }
        public async Task<bool> CheckIfAdminAsync(ClaimsPrincipal userPrincipal)
        {
            var userData = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.UserId == userPrincipal.Identity.Name);
            return false;
        }
    }
}
