namespace ExtremeWeatherBoard.DTO
{
    public class MainNavContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public PageRedirectionDTO? SubCategoryRedirectionDTO { get; set; } 
    }
}
