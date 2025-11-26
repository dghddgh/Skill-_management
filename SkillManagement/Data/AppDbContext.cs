using Microsoft.EntityFrameworkCore;
using SkillManagement.Models;

namespace SkillManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.DisplayName).HasMaxLength(100);

            entity.OwnsMany(p => p.Skills, skill =>
            {
                skill.ToJson();
                skill.Property(s => s.Name).HasMaxLength(50);
                skill.Property(s => s.Level).IsRequired();
            });
        });
    }
}