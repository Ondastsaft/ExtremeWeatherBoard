using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]  
        public string? Title { get; set; }
        [Required]
        public string? CreatorId { get; set; }
        public AdminUser? Creator { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

    }
}
