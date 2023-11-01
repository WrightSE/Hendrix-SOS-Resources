using SOSResources.Models;
using Microsoft.EntityFrameworkCore;

namespace SOSResources.Data
{
    public class SOSContext : DbContext
    {
        public SOSContext(DbContextOptions<SOSContext> options) : base(options)
        {
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<ResourceRequest> ResourceRequests { get; set; }
        public DbSet<TextbookRequest> TextbookRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Participant>().ToTable(nameof(Course))
            //     .HasMany(c => c.Instructors)
            //     .WithMany(i => i.Courses);
            // modelBuilder.Entity<Student>().ToTable(nameof(Student));
            // modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));
        }
    }
}