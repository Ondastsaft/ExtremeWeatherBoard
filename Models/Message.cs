using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string? Topic { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public DateTime SentAt { get; set; }
        [Required]
        public string? SenderId { get; set; }
        public User? Sender { get; set; }
        [Required]
        public string? ReceiverId { get; set; }
        public User? Receiver { get; set; }
    }
}
