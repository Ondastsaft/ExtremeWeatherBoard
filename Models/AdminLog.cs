using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class AdminLog
    {
        public int Id { get; set; }
        [Required]
        public string? Action { get; set; }
        [Required]
        public string? Details { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int AdminUserDataId { get; set; }
        public AdminUserData? AdminUserData { get; set; }
    }
}
