namespace ExtremeWeatherBoard.Models
{
    public class AdminUser
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<AdminLog> AdminLog { get; set; }
    }
}
