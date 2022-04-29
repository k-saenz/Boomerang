using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boomerang.Models;
using Boomerang.Models.Items;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boomerang.Data
{
    public class BoomerangDbContext : IdentityDbContext<User>
    {
        public BoomerangDbContext(DbContextOptions<BoomerangDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<BoomerangFile>()
                .HasOne(fd => fd.FileData)
                .WithOne(b => b.File)
                .HasForeignKey<FileData>(fd => fd.BoomerangFileId);
        }

        public DbSet<BoomerangFile> Files { get; set; }
        public DbSet<FileData> FileData { get; set; }
        
        
    }
}
