using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public bool? Read { get; set; }
        public DateTime SentAt { get; set; }
        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual UserData? Sender { get; set; }
        public int? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual UserData? Receiver { get; set; }
    }
}
