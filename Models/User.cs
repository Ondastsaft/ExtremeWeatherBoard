using Microsoft.AspNetCore.Identity;

namespace ExtremeWeatherBoard.Models
{
    public class User:IdentityUser
    {        
        public ICollection<Thread>? Threads { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }

    }
}
