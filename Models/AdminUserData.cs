using ExtremeWeatherBoard.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class AdminUserData : IUser
    {
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
        [InverseProperty("DiscussionThreadAdminUserData")]
        public virtual ICollection<DiscussionThread>? DiscussionThreads { get; set; }
        [InverseProperty("CommentAdminUserData")]
        public virtual ICollection<Comment>? Comments { get; set; }
        [InverseProperty("AdminReceiver")]
        public virtual ICollection<Message>? ReceivedMessages { get; set; }
        [InverseProperty("AdminSender")]
        public virtual ICollection<Message>? SentMessages { get; set; }
        [InverseProperty("CreatorAdminUser")]
        public virtual ICollection<Category>? Categories { get; set; }
        [InverseProperty("SubCategoryAdminUserData")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        [InverseProperty("LogsAdminUserData")]
        public virtual ICollection<AdminLog>? AdminLogs { get; set; }
    }
}
