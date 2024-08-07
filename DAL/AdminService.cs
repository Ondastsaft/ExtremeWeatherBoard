﻿using ExtremeWeatherBoard.Data;
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
        public AdminService
            (
            ApplicationDbContext context
            , UserDataService userDataService
            , CategoryApiService categoryAPIService
            , SubCategoryService subcategoryService
            , CommentService commentService
            , AdminLogService adminLogService
            , UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            _userDataService = userDataService;
            _categoryAPIService = categoryAPIService;
            _subcategoryService = subcategoryService;
            _commentService = commentService;
            _adminLogService = adminLogService;
            _userManager = userManager;
        }
        public async Task PostCategoryAsync(string title, ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity?.Name != null)
            {
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userPrincipal.Identity.Name);
                if (adminUserData != null)
                {
                    await _categoryAPIService.PostCategoryAsync(title, adminUserData);
                }
            }
        }
        public async Task DeleteCategoryAsync(int categoryId, ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity?.Name != null)
            {
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userPrincipal.Identity.Name);
                if (adminUserData != null)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        await _context.SaveChangesAsync();
                    }
                }
            }

        }
        public async Task PostSubCategoryAsync(string title, int parentCategoryId, ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity?.Name != null)
            {
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userPrincipal.Identity.Name);
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
        public async Task DeleteSubCategoryAsync(int subCategoryId, ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal.Identity?.Name != null)
            {
                var adminUserData = await _context.AdminUserDatas.FirstOrDefaultAsync(ud => ud.UserId == userPrincipal.Identity.Name);
                if (adminUserData != null)
                {
                    var subCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == subCategoryId);
                    if (subCategory != null)
                    {
                        _context.SubCategories.Remove(subCategory);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task PostAdminUserDataAsync(string userId)
        {
            var adminUserData = new AdminUserData() { UserId = userId };
            _context.AdminUserDatas.Add(adminUserData);
            await _context.SaveChangesAsync();
        }

    }
}
