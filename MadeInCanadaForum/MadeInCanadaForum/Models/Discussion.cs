using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeInCanadaForum.Models
{

    public class Discussion
    {
        // Primary key
        public int DiscussionId { get; set; }
        public string Content { get; set; } = string.Empty;


        public string Location { get; set; } = string.Empty;

        public string Camera { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        // property for file upload, not mapped in EF
        [NotMapped]
        [Display(Name = "Photograph")]

        public IFormFile? ImageFile { get; set; } // nullable




        [Display(Name = "Visibility")]

        public bool IsVisible { get; set; } = false;


        [Display(Name = "Date Created")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string Title { get; set; } = string.Empty;



        //Navigation property
        public List<Comment>? Comments { get; set; } //nullable
    }


}
