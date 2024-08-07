using Microsoft.AspNetCore.Mvc;

namespace ExtremeWeatherBoard
{
    public static class PageRedirection
    {
        public static IActionResult RedirectToDiscussionThreads(int subCategoryId)
        {
            if (subCategoryId <= 0)
            {
                return new BadRequestResult();
            }
            return new RedirectToPageResult("DiscussionThreads", new { subCategoryId });
        }
    }
}
