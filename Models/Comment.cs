using ExtremeWeatherBoard.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ExtremeWeatherBoard.Models
{
    public class Comment : IMainContent
    {
        public int Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Title { get; set; }
        public bool IsReported { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? CommentUserDataId { get; set; }
        [ForeignKey("CommentUserDataId")]
        public virtual UserData? CommentUserData { get; set; }
        public int ParentDiscussionThreadId { get; set; }
        [ForeignKey("ParentDiscussionThreadId")]
        public DiscussionThread? ParentDiscussionThread { get; set; }
    }
}
