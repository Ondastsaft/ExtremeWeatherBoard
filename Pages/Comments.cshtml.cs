using ExtremeWeatherBoard.DAL;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.ViewModels.Shared;    
using ExtremeWeatherBoard.Pages.PageModels;


namespace ExtremeWeatherBoard.Pages
{
    public class CommentsModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;
        MainContentCardsViewModel MainContent { get; set; } = new MainContentCardsViewModel();
        public CommentsModel(SubCategoryService subCategoryService, CommentService commentService, DiscussionThreadService discussionThreadService)
        {
            _subCategoryService = subCategoryService;
            _commentService = commentService;
            _discussionThreadService = discussionThreadService;

        }
        public async Task OnGetAsync(int sidebarContentId, int mainContentId)
        {

            var subCategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (subCategories !=null)
            {
                SideBarOptions = new SideBarPartialViewModel() { NavigateTo = "/DiscussionThreads", SideBarOptions = subCategories.Cast<ISideBarOption>().ToList() };
            }
            var comments = await _commentService.GetCommentsAsync(mainContentId);           
            if (comments != null)
            {
                var parentDiscussionThread = await _discussionThreadService.GetDiscussionThreadAsync(mainContentId);
                {
                    MainContent.MainContentList = comments.Count > 0 ? comments.Cast<IMainContent>().ToList() : new List<IMainContent>();
                };
            }
        }
    }
}
