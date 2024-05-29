using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.AdminUserDataAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public DeleteModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdminUserData AdminUserData { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminuserdata = await _context.AdminUserDatas.FirstOrDefaultAsync(m => m.Id == id);

            if (adminuserdata == null)
            {
                return NotFound();
            }
            else
            {
                AdminUserData = adminuserdata;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminuserdata = await _context.AdminUserDatas.FindAsync(id);
            if (adminuserdata != null)
            {
                AdminUserData = adminuserdata;
                _context.AdminUserDatas.Remove(AdminUserData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
