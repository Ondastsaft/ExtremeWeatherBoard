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
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment? Comment { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public int? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory? SubCategory { get; set; }
    }
}
