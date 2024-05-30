using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class AdminUserData
    {
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        [NotMapped]
        public virtual MockIdentityUser? MockUser { get; set; }
        [NotMapped]
        public string? MockUserId { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
        [Required]
        public virtual ICollection<DiscussionThread>? Threads { get; set; }
        [InverseProperty("CommentAdminUserData")]
        public virtual ICollection<Comment>? Comments { get; set; }
        [InverseProperty("AdminReceiver")]
        public virtual ICollection<Message>? ReceivedMessages { get; set; }
        [InverseProperty("AdminSender")]
        public virtual ICollection<Message>? SentMessages { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Category>? Categories { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        [InverseProperty("LogsAdminUserData")]
        public virtual ICollection<AdminLog>? AdminLogs { get; set; }
    }
}
