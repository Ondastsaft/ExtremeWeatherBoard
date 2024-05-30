using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace ExtremeWeatherBoard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            var result = await CheckUserAsync();
        }
        public async Task<IActionResult> CheckUserAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await _context.UserDatas.AnyAsync(u => u.UserId == userId))
                {
                    return new JsonResult("User exists in UserDatas");
                }
                if (await _context.AdminUserDatas.AnyAsync(a => a.UserId == userId))
                {
                    return new JsonResult("User exists in AdminUserDatas");
                }
                else
                {
                    var userData = new UserData() { UserId = userId };
                    await _context.UserDatas.AddAsync(userData);
                    await _context.SaveChangesAsync();
                    return new JsonResult("User Data added to database");
                }
            }
            return new JsonResult("Model state is not valid");
        }
    }    
}
