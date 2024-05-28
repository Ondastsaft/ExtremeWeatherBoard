using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremeWeatherBoard.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required] 
        public string? Name { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public DateTime PostedAt { get; set; }
        public int CommentUserDataId { get; set; }
        [Required]
        [ForeignKey("CommentUserDataId")]
        public UserData? CommentUserData { get; set; }
        [Required]

        public int CommentThreadId { get; set; }
        [ForeignKey("CommentThreadId")]
        public Thread? CommentThread { get; set; }
    }
}
