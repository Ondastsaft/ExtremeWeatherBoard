using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Models
{
    public class Category : IContent
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [InverseProperty("ParentCategory")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        public int CreatorAdminUserDataId { get; set; }
        [ForeignKey("CreatorAdminUserDataId")]
        public virtual AdminUserData? CreatorAdminUserData { get; set; }
    }
}
