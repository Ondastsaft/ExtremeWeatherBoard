using Microsoft.AspNetCore.Identity;

namespace ExtremeWeatherBoard.Models
{
    public class AdminUserData : UserData
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<AdminLog> AdminLog { get; set; }
    }
}
