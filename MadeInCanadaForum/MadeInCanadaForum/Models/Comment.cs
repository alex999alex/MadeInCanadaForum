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

        // Foreign key for Discussion
        public int DiscussionId { get; set; }

        // Foreign key for ApplicationUser
        public string? ApplicationUserId { get; set; }

        // Navigation properties
        public Discussion? Discussion { get; set; }  //nullable
        public ApplicationUser? ApplicationUser { get; set; }  //nullable
    }
}
