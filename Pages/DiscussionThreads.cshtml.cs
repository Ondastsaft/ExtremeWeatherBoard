using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExtremeWeatherBoard.DTO;
using System.Globalization;
namespace ExtremeWeatherBoard.Pages
{
    public class DiscussionThreadsModel : PageModel
    {
        public List<DiscussionThreadDTO> DiscussionThreads { get; set; } = new();
        public string SubCategoryTitle { get; set; } = String.Empty;
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        public int SubCategoryId { get; set; }
        public DiscussionThreadsModel(
            SubCategoryService subCategoryService, 
            DiscussionThreadService discussionThreadService
            )
        {
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
        }
        public async Task OnGetAsync(int subCategoryId)
        {
            SubCategoryId = subCategoryId;
            var subCategory = await _subCategoryService.GetSubCategoryAsync(subCategoryId);
            if (subCategory != null)
            {
                SubCategoryTitle = subCategory.Title ?? "Title not found";
            }
            await LoadDiscussionThreads(subCategoryId);
        }
        public async Task LoadDiscussionThreads(int subCategoryId)
        {
            var discussionThreads = await _discussionThreadService.GetDiscussionThreadsAsync(subCategoryId);
            if (discussionThreads != null)
            {
                foreach(var discussionThread in discussionThreads)
                {                  
                    DiscussionThreads.Add(new DiscussionThreadDTO()
                    {
                        Id = discussionThread.Id,
                        Title = discussionThread.Title == null ? "title not found" : discussionThread.Title,
                        Text = discussionThread.Text == null ? "text not found" : discussionThread.Text,
                        UserDataId = discussionThread.DiscussionThreadUserDataId,
                        UserName = discussionThread.DiscussionThreadUserData?.Name ?? "User name not found",
                        ImageUrl = discussionThread.DiscussionThreadUserData?.ImageURL ?? "User image URL not found",
                        TimeStamp = discussionThread.TimeStamp.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture),
                        SubCategoryId = discussionThread.SubCategoryId
                    });
                }
            }
        }
    }
}
