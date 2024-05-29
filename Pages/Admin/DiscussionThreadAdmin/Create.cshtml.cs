using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.DiscussionThreadAdmin
{
    public class CreateModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public CreateModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CreatorUserId"] = new SelectList(_context.AdminUserDatas, "Id", "Id");
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public DiscussionThread DiscussionThread { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DiscussionThreads.Add(DiscussionThread);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
