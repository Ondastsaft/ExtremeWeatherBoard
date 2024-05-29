using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]        
        public virtual UserData? Sender { get; set; }
        public int? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual UserData? Receiver { get; set; }
        public int? AdminSenderId { get; set; }
        [ForeignKey("AdminSenderId")]
        public virtual AdminUserData? AdminSender { get; set; }
        public int? AdminReceiverId { get; set; }
        [ForeignKey("AdminReceiverId")]
        public virtual AdminUserData? AdminReceiver { get; set; }
    }
}
