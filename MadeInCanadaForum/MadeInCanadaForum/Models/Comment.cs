using System.ComponentModel.DataAnnotations;

namespace MadeInCanadaForum.Models
{
    public class Comment
    {
        // Primary key
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Posted")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Foreign keys
        public int DiscussionId { get; set; }
        public string? ApplicationUserId { get; set; }
        public int? ParentCommentId { get; set; }  // For nested replies

        // Navigation properties
        public Discussion? Discussion { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public Comment? ParentComment { get; set; }
        public List<Comment>? Replies { get; set; }
        
        // Voting
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}
