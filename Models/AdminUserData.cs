using ExtremeWeatherBoard.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class AdminUserData
    {
        public int Id { get; set; }        
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        [InverseProperty("CreatorAdminUserData")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        [InverseProperty("LogsAdminUserData")]
        public virtual ICollection<AdminLog>? AdminLogs { get; set; }
    }
}
