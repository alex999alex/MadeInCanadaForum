namespace MadeInCanadaForum.Models
{

    public class Discussion
    {
        // Primary key
        public int DiscussionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreateDate { get;set; }

        public bool IsPublic { get; set; } = false;

        //Navigation property
        public List<Comment>? Comments { get; set; } //nullable
    }


}
