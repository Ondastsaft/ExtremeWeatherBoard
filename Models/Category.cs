using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required] 
        public string? Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<SubCategory>? SubCategories { get; set; }
        [Required]
        public string? CreatorID { get; set; }
        public AdminUser? Creator { get; set; }

    }
}
