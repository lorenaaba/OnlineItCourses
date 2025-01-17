using Microsoft.EntityFrameworkCore;
using OnlineITCourses.Models;

namespace OnlineITCourses.Data;

public class OnlineITCoursesDbContext : DbContext
{
    public OnlineITCoursesDbContext(DbContextOptions<OnlineITCoursesDbContext> options) : base(options)
    {
    }
    
    public DbSet<Tecaj> Tecajevi { get; set; }
    public DbSet<Grupa> Grupe { get; set; }
    public DbSet<Instruktor> Instruktori { get; set; }
    public DbSet<InstruktorTecaj> InstruktorTecajevi { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InstruktorTecaj>()
            .HasKey(it => new { it.InstruktorId, it.TecajId });

        modelBuilder.Entity<InstruktorTecaj>()
            .HasOne(it => it.Instruktor)
            .WithMany(i => i.Tecaj)
            .HasForeignKey(it => it.InstruktorId);

        modelBuilder.Entity<InstruktorTecaj>()
            .HasOne(it => it.Tecaj)
            .WithMany(t => t.Instruktori)
            .HasForeignKey(it => it.TecajId);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        modelBuilder.Entity<Grupa>()
            .HasIndex(g=> g.Naziv)
            .IsUnique();

        modelBuilder.Entity<Log>()
            .Property(l => l.Timestamp);
        //.HasDefaultValueSql("GETUTCDATE()");

    }
}