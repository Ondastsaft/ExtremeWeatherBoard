using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Models
{
    public class DiscussionThread:IMainContent
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }    
        public DateTime CreatedAt { get; set; }
        public int? DiscussionThreadAdminUserDataId { get; set; }
        [ForeignKey("DiscussionThreadAdminUserDataId")]
        public AdminUserData? DiscussionThreadAdminUserData { get; set; }
        public int? DiscussionThreadUserDataId { get; set; }
        [ForeignKey("DiscussionThreadUserDataId")]
        public virtual UserData? DiscussionThreadUserData { get; set; }
        public int SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public SubCategory? SubCategory { get; set; }
        [InverseProperty("CommentThread")]
        public ICollection<Comment>? Comments { get; set; }
    }
}
