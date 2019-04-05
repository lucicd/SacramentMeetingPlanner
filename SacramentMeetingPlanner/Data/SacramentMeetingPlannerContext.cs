using SacramentMeetingPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
