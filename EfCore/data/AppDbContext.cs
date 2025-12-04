using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InterestGroup> InterestGroups { get; set; }
        public DbSet<UserInterestGroup> UserInterestGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql.ects;Database=MenshDBfin;User Id=student_12;Password=student_12;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HomeDB;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.ConfigureWarnings(warnings =>
                  warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<UserInterestGroup>()
                .HasKey(cs => new { cs.UserId, cs.InterestGroupId });

            modelBuilder.Entity<UserInterestGroup>()
                .HasOne(cs => cs.User)
                .WithMany(s => s.UserInterestGroups)
                .HasForeignKey(cs => cs.UserId);

            modelBuilder.Entity<UserInterestGroup>()
                .HasOne(cs => cs.InterestGroup)
                .WithMany(c => c.UserInterestGroups)
                .HasForeignKey(cs => cs.InterestGroupId);
        }
    }
}
