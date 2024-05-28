using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]  
        public string? Title { get; set; }
        public int CreatorId { get; set; }
        public AdminUserData? Creator { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

    }
}
