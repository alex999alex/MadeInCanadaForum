﻿using System.ComponentModel.DataAnnotations;
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
        public bool IsVisible { get; set; } = true;

        [Display(Name = "Posted")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        // Foreign key for ApplicationUser
        public string? ApplicationUserId { get; set; }

        // Navigation properties
        public List<Comment>? Comments { get; set; } //nullable
        public ApplicationUser? ApplicationUser { get; set; } //nullable
    }
}
