namespace MadeInCanadaForum.Models
{

    public class Comment
    {
        // Primary key
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        // Foreign key
        public int DiscussionId { get; set; }

        // Navigation property
        public Discussion? Discussion { get; set; }  //nullable
    }
}
