namespace ExtremeWeatherBoard.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserDataId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;
        public bool IsReported { get; set; }
    }
}
