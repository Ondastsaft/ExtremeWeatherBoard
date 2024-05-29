using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExtremeWeatherBoard.Data;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Admin.CommentAdmin
{
    public class IndexModel : PageModel
    {
        private readonly ExtremeWeatherBoard.Data.ApplicationDbContext _context;

        public IndexModel(ExtremeWeatherBoard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Comment = await _context.Comments
                .Include(c => c.CommentAdminUserData)
                .Include(c => c.CommentThread)
                .Include(c => c.CommentUserData).ToListAsync();
        }
    }
}
