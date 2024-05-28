using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required] 
        public string? Name { get; set; }
        [Required]
        public virtual DateTime CreatedDate { get; set; }
        [Required]
        public string? Title { get; set; }
        [InverseProperty("ParentCategory")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual AdminUserData? Creator { get; set; }
    }
}
