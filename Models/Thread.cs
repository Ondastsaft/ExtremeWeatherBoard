using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class Thread
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
