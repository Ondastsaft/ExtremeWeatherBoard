using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.DiscussionThreadAdmin
{
    public class EditModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public EditModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DiscussionThread DiscussionThread { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionthread =  await _context.DiscussionThreads.FirstOrDefaultAsync(m => m.Id == id);
            if (discussionthread == null)
            {
                return NotFound();
            }
            DiscussionThread = discussionthread;
           ViewData["CreatorUserId"] = new SelectList(_context.AdminUserDatas, "Id", "Id");
           ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DiscussionThread).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionThreadExists(DiscussionThread.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DiscussionThreadExists(int id)
        {
            return _context.DiscussionThreads.Any(e => e.Id == id);
        }
    }
}
