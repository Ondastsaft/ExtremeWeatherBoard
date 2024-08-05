namespace ExtremeWeatherBoard.Pages.Topic
{
    using Microsoft.AspNetCore.Mvc;
    public class TopicPostPartialViewModel
    {
        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Text { get; set; }
    }
}
