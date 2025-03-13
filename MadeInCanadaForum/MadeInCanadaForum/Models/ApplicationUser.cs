using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeInCanadaForum.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Location")]
        public string? Location { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ImageFilename { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; }

        // Navigation properties
        public virtual ICollection<Discussion>? Discussions { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
} 