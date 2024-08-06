namespace ExtremeWeatherBoard.DTO
{
    public class DiscussionThreadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int UserDataId { get; set; }
        public int SubCategoryId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;
    }
}
