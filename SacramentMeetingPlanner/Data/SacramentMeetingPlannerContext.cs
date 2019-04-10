using SacramentMeetingPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SacramentMeetingPlanner.Data
{
    public class SacramentMeetingPlannerContext : IdentityDbContext
    {
        public SacramentMeetingPlannerContext(DbContextOptions<SacramentMeetingPlannerContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<Speaker>().ToTable("Speaker");
            modelBuilder.Entity<Setting>().ToTable("Setting");

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
        }
    }
}
