using ExtremeWeatherBoard;
using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.Pages.Shared.Views
{
    public class MainContentListItemPartialViewModel
    {
        public string Title { get; set; } = string.Empty;
        public int TitleId { get; set; }
        public string TitleNavigate { get; set; } = string.Empty;
        public string[,] ContentArray { get; set; } = new string[4, 3];
        public string ContentNavigate { get; set; } = string.Empty;
        private string ErrorMessage { get; set; } = "Property null";
        public MainContentListItemPartialViewModel()
        {
        }
        public void UnpackItem(IContent item)
        {
            if (item != null)
            {
                switch (item)
                {
                    case Category category:
                        UnpackCategory(category);
                        break;
                    case SubCategory subCategory:
                        UnpackSubCategory(subCategory);
                        break;
                }
            }
        }
        private void UnpackCategory(Category category)
        {
            Title = category.Title ?? ErrorMessage;
            TitleId = category.Id;
            TitleNavigate = "/Index";
            ContentNavigate = "/Topic/SubCategoryIndex/";
            var subCategories = category.SubCategories?.ToList();
            if (subCategories != null && subCategories.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    ContentArray[i, 0] = subCategories[subCategories.Count - (1 + i)].Title ?? ErrorMessage;
                    ContentArray[i, 1] = $"{subCategories[subCategories.Count - (1 + i)].Id}";
                }
            }
        }
        private void UnpackSubCategory(SubCategory subCategory)
        {
            Title = subCategory.Title ?? ErrorMessage;
            TitleId = subCategory.Id;
            TitleNavigate = "/SubCategories";
            if (subCategory.Threads != null && subCategory.Threads.Count >= 3)
            {
                var threads = subCategory.Threads.ToList();
                for (int i = 0; i < 3; i++)
                {
                    ContentArray[i, 0] = threads[threads.Count - (1 + i)].Title ?? ErrorMessage;
                    ContentArray[i, 1] = $"{threads[threads.Count - (1 + i)].Id}";
                }
            }
        }
    }
}
