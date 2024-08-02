namespace ExtremeWeatherBoard.Pages.Discussion
{
    using Microsoft.AspNetCore.Mvc;
    public class DiscussionThreadPostPartialViewModel
    {
        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Text { get; set; }
    }
}
