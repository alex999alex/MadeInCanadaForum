using System.ComponentModel.DataAnnotations;

namespace MadeInCanadaForum.Models
{

    public class Discussion
    {
        // Primary key
        public int DiscussionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [Display(Name = "Date Created")]
        public DateTime CreateDate { get;set; }

        public string ImageFilename { get; set; } = string.Empty;

        [Display(Name = "Discussion Visibility")]

        public bool IsPublic { get; set; } = false;

        //Navigation property
        public List<Comment>? Comments { get; set; } //nullable
    }


}
