using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MadeInCanadaForum.Models;

namespace MadeInCanadaForum.Data
{
    public class MadeInCanadaForumContext : IdentityDbContext<ApplicationUser>
    {
        public MadeInCanadaForumContext (DbContextOptions<MadeInCanadaForumContext> options)
            : base(options)
        {
        }

        public DbSet<MadeInCanadaForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<MadeInCanadaForum.Models.Comment> Comment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Comment and Discussion
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionId)
                .OnDelete(DeleteBehavior.NoAction); // Changed from Cascade to NoAction
        }
    }
}
