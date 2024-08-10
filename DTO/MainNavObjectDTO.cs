namespace ExtremeWeatherBoard.DTO
{
    using ExtremeWeatherBoard.Models;
    public class MainNavObjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public List<MainNavContentDTO> NavContentDTOs { get; set; } = new();
    }
}
