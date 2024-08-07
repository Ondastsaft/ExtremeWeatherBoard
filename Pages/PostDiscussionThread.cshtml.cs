using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Pages
{
    public class PostDiscussionThreadModel : PageModel
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DiscussionThreadService _discussionThreadService;
        private readonly SubCategoryService _subCategoryService;

        public PostDiscussionThreadModel(
            CategoryApiService categoryApiService,
            UserManager<IdentityUser> userManager,
            DiscussionThreadService discussionThreadService,
            SubCategoryService subCategoryService
            )
        {
            _categoryApiService = categoryApiService;
            _userManager = userManager;
            _discussionThreadService = discussionThreadService;
            _subCategoryService = subCategoryService;
        }
        [BindProperty]
        public int SubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Title can't exceed 40 characters")]
        [BindProperty]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Text can't exceed 2000 characters")]
        [BindProperty]
        public string Text { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync(int subCategoryId)
        {
            if (subCategoryId != 0)
            {
                this.SubCategoryId = subCategoryId;
                var subcategory = await _subCategoryService.GetSubCategory(SubCategoryId);
                if (subcategory.Title != null)
                {
                    SubCategoryTitle = subcategory.Title;
                    return Page();
                }
            }
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _discussionThreadService.PostDiscussionThreadAsync(User, SubCategoryId, Title, Text);
            return RedirectToPage("DiscussionThreads", new { subCategoryId = SubCategoryId });
        }

    }
}
