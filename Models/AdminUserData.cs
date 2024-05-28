using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class AdminUserData : UserData
    {

        [InverseProperty("Creator")]
        public virtual ICollection<Category>? Categories { get; set; }

        [InverseProperty("Creator")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        [InverseProperty("LogsAdminUserData")]
        public virtual ICollection<AdminLog>? AdminLogs { get; set; }
    }
}
