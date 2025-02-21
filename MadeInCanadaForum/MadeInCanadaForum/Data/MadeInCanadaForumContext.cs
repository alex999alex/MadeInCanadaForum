using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MadeInCanadaForum.Models;

namespace MadeInCanadaForum.Data
{
    public class MadeInCanadaForumContext : DbContext
    {
        public MadeInCanadaForumContext (DbContextOptions<MadeInCanadaForumContext> options)
            : base(options)
        {
        }

        public DbSet<MadeInCanadaForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<MadeInCanadaForum.Models.Comment> Comment { get; set; } = default!;
    }
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; } // Add this property to store the image path
        public DateTime CreateDate { get; set; }
    }
}
