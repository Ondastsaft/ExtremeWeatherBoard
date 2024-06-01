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
        public async Task OnGetAsync(int id)
        {
            this.SideBarOptions = new SideBarPartialViewModel();
            SideBarOptions.SideBarOptions = (await _subCategoryService.GetSubCategoriesAsync(id))
                .Cast<ISideBarOption>()
                .ToList()
                ;
        }
    }
}
