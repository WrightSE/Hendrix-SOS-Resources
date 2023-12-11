using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Areas.Identity.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Data;

public class ApplicationDbContext : IdentityDbContext<SOS_User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Textbook> Textbooks { get; set; }
    public DbSet<Copy> Copies { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceType> ResourceTypes { get; set; }
    public DbSet<TextbookRequest> TextbookRequests { get; set; }
    public DbSet<ResourceRequest> ResourceRequests { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Textbook>().ToTable(nameof(Textbook));
        modelBuilder.Entity<Copy>().ToTable(nameof(Copy));
        modelBuilder.Entity<Resource>().ToTable(nameof(Resource));
        modelBuilder.Entity<ResourceType>().ToTable(nameof(ResourceType));
        modelBuilder.Entity<TextbookRequest>().ToTable(nameof(TextbookRequest));
        modelBuilder.Entity<ResourceRequest>().ToTable(nameof(ResourceRequest));
        modelBuilder.Entity<Participant>().ToTable(nameof(Participant));
    }
}
