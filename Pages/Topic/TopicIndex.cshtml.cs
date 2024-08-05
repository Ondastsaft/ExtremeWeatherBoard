using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Pages.PageModels;
using ExtremeWeatherBoard.Interfaces;
namespace ExtremeWeatherBoard.Pages.Topic;

public class TopicIndexModel : BasePageModel
{
    private readonly SubCategoryService _subCategoryService;
    private readonly DiscussionThreadService _discussionThreadService;
    public TopicPostPartialViewModel? DiscussionThreadsPostModel { get; set; }
    public TopicIndexModel(
        SubCategoryService subCategoryService, 
        DiscussionThreadService discussionThreadService, 
        UserDataService userDataService
        ) : base(userDataService)
    {
        _subCategoryService = subCategoryService;
        _discussionThreadService = discussionThreadService;
    }
    public override async Task OnGetAsync(int sidebarContentId, int mainContentId)
    {
        MainContentId = mainContentId;
        SideBarContentId = sidebarContentId;
        if (Request.Query.ContainsKey("post"))
        {
            DiscussionThreadsPostModel = new TopicPostPartialViewModel();
        }
        else
        {
            await LoadMainContent();
        }
    }

    protected override async Task LoadMainContent()
    {
        PageMainContentPartialModel.NavigateTo = "/Discussion/Comments";
        PageMainContentPartialModel.MainContentList = (await _discussionThreadService.GetDiscussionThreadsAsync(MainContentId))
            .Cast<IContent>().
            ToList();
    }
    protected override async Task LoadSideBar()
    {
        PageSideBarPartialModel.NavigateTo = "/SubCategory/SubCategoryIndex";
        PageSideBarPartialModel.SideBarOptions = (await _subCategoryService.GetSubCategoriesFromParentIdAsync(SideBarContentId))
            .Cast<IContent>(). 
            ToList();

    }  
}
