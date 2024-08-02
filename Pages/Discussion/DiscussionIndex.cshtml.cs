using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.ViewModels.Shared;

namespace ExtremeWeatherBoard.Pages.Discussion;

public class DiscussionIndexModel : BasePageModel
{
    private readonly SubCategoryService _subCategoryService;
    private readonly DiscussionThreadService _discussionThreadService;
    public MainContentCardsViewModel MainContent { get; set; } = new MainContentCardsViewModel();
    public DiscussionThreadPostPartialViewModel? DiscussionThreadsPostModel { get; set; }
    public DiscussionIndexModel(SubCategoryService subCategoryService, DiscussionThreadService discussionThreadService)
    {
        _subCategoryService = subCategoryService;
        _discussionThreadService = discussionThreadService;
    }
    public async Task OnGetAsync(int sidebarContentId, int mainContentId, bool post)
    {
        await LoadSidebar(sidebarContentId);
        if (!post)
        {
            await LoadMainContent(mainContentId);
        }
        else
        {
            DiscussionThreadsPostModel = new DiscussionThreadPostPartialViewModel();
        }
    }
    public async Task LoadSidebar(int sideBarContentId)
    {
        SideBarOptions = new SideBarPartialViewModel();
        SideBarOptions.NavigateTo = "/Discussion/Index";
        var sidebarOptions = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sideBarContentId);
        if (sidebarOptions != null)
        {
            SideBarOptions.SideBarOptions = sidebarOptions.Cast<ISideBarOption>().ToList();
        }
    }
    public async Task LoadMainContent(int mainContentId)
    {
        MainContent = new MainContentCardsViewModel();
        MainContent.MainContentList = (await _discussionThreadService.GetDiscussionThreadsAsync(mainContentId))
            .Cast<IMainContent>().
            ToList();
    }
}
