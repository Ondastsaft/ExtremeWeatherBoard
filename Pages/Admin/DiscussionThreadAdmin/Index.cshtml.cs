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
    public class IndexModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public IndexModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DiscussionThread> DiscussionThread { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DiscussionThread = await _context.DiscussionThreads
                .Include(d => d.CreatorUser)
                .Include(d => d.SubCategory).ToListAsync();
        }
    }
}
