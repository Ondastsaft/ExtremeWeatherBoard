using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.DiscussionThreadAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public DeleteModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
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

            var discussionthread = await _context.DiscussionThreads.FirstOrDefaultAsync(m => m.Id == id);

            if (discussionthread == null)
            {
                return NotFound();
            }
            else
            {
                DiscussionThread = discussionthread;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionthread = await _context.DiscussionThreads.FindAsync(id);
            if (discussionthread != null)
            {
                DiscussionThread = discussionthread;
                _context.DiscussionThreads.Remove(DiscussionThread);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
