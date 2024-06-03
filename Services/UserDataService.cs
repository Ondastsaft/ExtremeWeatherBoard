using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Services
{
    public class UserDataService
    {
        private readonly DataRepository _dataRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private UserData? _currentUserData;
        public UserDataService(UserManager<IdentityUser> userManager, DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _userManager = userManager;
        }
        //public async Task<bool> CheckUserState(ClaimsPrincipal userPrincipal)
        //{
        //    bool confirmed = await CheckCurrentUserAsync(userPrincipal);
        //    confirmed = await CheckUserDataAsync(userPrincipal);
        //    _isLoggedIn = confirmed;
        //    if (_isAdmin) { return false; }
        //    return confirmed;
        //}
        public async Task<bool> CheckCurrentUserAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity.IsAuthenticated)
            {

                var userId = _userManager.GetUserId(userPrincipal);
                //UserData userData = new UserData() { UserId = userId };
                //await _dataRepository.AddUserDataAsync(userData);
                //return false;
                await _dataRepository.PopulateUserDatasAsync();
                if (_dataRepository.Users.Any(u => u.UserId == userId))
                {
                    return true;
                }
                //await _dataRepository.PopulateAdminUserDatasAsync();
                //if (_dataRepository.AdminUsers.Any(a => a.UserId == userId))
                //{
                //    AdminUserData adminUserData = new AdminUserData() { UserId = userId };
                //    _dataRepository.AddAdminUserDataAsync(adminUserData);
                //    return false;
                //}
                else
                {
                    UserData newUserData = new UserData() { UserId = userId };
                    await _dataRepository.AddUserDataAsync(newUserData);
                    return true;
                }
            }
            else { return false; }
        }
        //        public async Task<bool> CheckUserDataAsync()
        //        {
        //            if (userPrincipal.Identity.IsAuthenticated)
        //            {
        //                var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        //                if (await _context.UserDatas.AnyAsync(u => u.UserId == userId))
        //                {
        //                    return true;
        //                }
        //                if (await _context.AdminUserDatas.AnyAsync(a => a.UserId == userId))
        //                {
        //                    return true;
        //                    _isAdmin = true;
        //                }
        //                else
        //                {
        //                    var userData = new UserData() { UserId = userId };
        //                    await _context.UserDatas.AddAsync(userData);
        //                    await _context.SaveChangesAsync();
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }
        //        public async Task<UserData> GetUserDataForCurrentUserAsync(ClaimsPrincipal userPrincipal)
        //        {
        //            var usersPopulated = await PopulateUsers();
        //            if (usersPopulated)
        //            {
        //                var userId = _userManager.GetUserId(userPrincipal);
        //                var userData = users.Find(x => x.UserId == userId);
        //                if (userData.GetType() == typeof(UserData))
        //                {
        //                    return userData;
        //                }

        //            }
        //        }
        //        public async Task<UserData> AddUserData(string userId)
        //        {
        //            var userData = new UserData() { UserId = userId };
        //            _context.UserDatas.Add(userData);
        //            await _context.SaveChangesAsync();
        //            return userData;
        //        }

    }
}
