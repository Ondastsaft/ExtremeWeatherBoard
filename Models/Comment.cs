using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required] 
        public string? Name { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public DateTime PostedAt { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int ThreadId { get; set; }
        public Thread? Thread { get; set; }
    }
}
