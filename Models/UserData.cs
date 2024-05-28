using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ExtremeWeatherBoard.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public IdentityUser? User { get; set; }
        [Required]
        public string? UserId { get; set; }
        public ICollection<Thread>? Threads { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public string? ImageURL { get; set; }

    }
}
