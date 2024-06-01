using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtremeWeatherBoard.Interfaces;

namespace ExtremeWeatherBoard.Models
{
    public class DiscussionThread:IMainContent, ISideBarOption
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }    
        public DateTime CreatedAt { get; set; }
        [Required]
        public int CreatorUserId { get; set; }
        [ForeignKey("CreatorUserId")]
        public AdminUserData? CreatorUser { get; set; }
        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory? SubCategory { get; set; }
        [InverseProperty("CommentThread")]
        public ICollection<Comment>? Comments { get; set; }
    }
}
