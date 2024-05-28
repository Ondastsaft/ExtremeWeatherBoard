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
        public UserData? Sender { get; set; }
        public UserData? Receiver { get; set; }
    }
}
