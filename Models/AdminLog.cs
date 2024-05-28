using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int LogsAdminUserDataId { get; set; }
        [ForeignKey("LogsAdminUserDataId")]
        public virtual AdminUserData? LogsAdminUserData { get; set; }
    }
}
