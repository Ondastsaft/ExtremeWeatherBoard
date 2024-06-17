using ExtremeWeatherBoard.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        IdentityUser? User { get;}
        string? UserId { get;}
        string? ImageURL { get; set; }      
        ICollection<DiscussionThread>? DiscussionThreads { get; set; }      
        ICollection<Comment>? Comments { get; set; }
        ICollection<Message>? ReceivedMessages { get; set; }
        ICollection<Message>? SentMessages { get; set; }
    }
}
