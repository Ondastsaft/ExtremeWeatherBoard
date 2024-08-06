using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExtremeWeatherBoard.DTO;
namespace ExtremeWeatherBoard.Pages
{
    public class DiscussionThreadsModel : BasePageModel
    {
        public List<DiscussionThreadDTO> DiscussionThreads { get; set; } = new();
        private readonly SubCategoryService _subCategoryService;
        private readonly DiscussionThreadService _discussionThreadService;
        public DiscussionThreadsModel(SubCategoryService subCategoryService, DAL.DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _discussionThreadService = discussionThreadService;
        }
        public async Task OnGetAsync(int sidebarContentId, int mainContentId)
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/DiscussionThreads";
            var sidebarOptions = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (sidebarOptions != null)
            {
                SideBarOptions.SideBarOptions = sidebarOptions.Cast<ISideBarOption>().ToList();
            }
            await LoadDiscussionThreads(mainContentId);
        }
        public async Task LoadDiscussionThreads(int mainContentId)
        {
            var discussionThreads = await _discussionThreadService.GetDiscussionThreadsAsync(mainContentId);
            if (discussionThreads != null)
            {
                foreach(var dt in discussionThreads)
                {
                    if(dt.DiscussionThreadUserData != null
                        && dt.DiscussionThreadUserData.Name != null
                        && dt.Title != null
                        && dt.Text != null)
                    DiscussionThreads.Add(new DiscussionThreadDTO()
                    {
                        Id = dt.Id,
                        SubCategoryId = dt.SubCategoryId,
                        Text = dt.Text,
                        Title = dt.Title,
                        UserDataId = dt.DiscussionThreadUserDataId,
                        UserName = dt.DiscussionThreadUserData.Name,
                        TimeStamp = dt.TimeStamp.ToString()
                    });
                }
            }
        }
    }
}
