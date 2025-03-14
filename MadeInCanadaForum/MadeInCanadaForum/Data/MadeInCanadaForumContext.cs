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
        public DbSet<MadeInCanadaForum.Models.CommentVote> CommentVote { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentVote>()
                .HasOne(v => v.Comment)
                .WithMany(c => c.Votes)
                .HasForeignKey(v => v.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentVote>()
                .HasIndex(v => new { v.CommentId, v.UserId })
                .IsUnique();
        }
    }
}
