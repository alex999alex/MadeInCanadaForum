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
}
