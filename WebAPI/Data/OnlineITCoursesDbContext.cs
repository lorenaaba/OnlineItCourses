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
}