using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.SubCategoryAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public DeleteModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.SubCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            else
            {
                SubCategory = subcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.SubCategories.FindAsync(id);
            if (subcategory != null)
            {
                SubCategory = subcategory;
                _context.SubCategories.Remove(SubCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
