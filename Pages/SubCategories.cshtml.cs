using ExtremeWeatherBoard.Services;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Pages.Shared.ViewModels;
using ExtremeWeatherBoard.Pages.PageModels;

namespace ExtremeWeatherBoard.Pages
{
    public class SubCategoriesModel : BasePageModel
    {
        private readonly SubCategoryService _subCategoryService;
        public SubCategoriesModel(SubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        public async Task OnGetAsync(int sidebarContentId)
        {
            SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.NavigateTo = "/SubCategories";
            SideBarOptions.SideBarOptions = (await _subCategoryService.GetSubCategoriesAsync(sidebarContentId))
                .Cast<ISideBarOption>()
                .ToList()
                ;
            int i = 0;
        }
    }
}
