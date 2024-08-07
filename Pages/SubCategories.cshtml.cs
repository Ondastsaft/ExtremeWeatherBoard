using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.DAL;
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
            var sidebarOptions = await _subCategoryService.GetSubCategoriesFromParentIdAsync(sidebarContentId);
            if (sidebarOptions != null)
            {
                SideBarOptions.SideBarOptions = sidebarOptions.Cast<ISideBarOption>().ToList();
            }
        }
    }
}
