using IssueOtter.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Project> Project { get; set; }
    public DbSet<Issue> Issue { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Label> Label { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project
        modelBuilder.Entity<Project>()
            .HasMany(x => x.Issues)
            .WithOne(p => p.Project)
            .HasForeignKey(x => x.ProjectId);

        modelBuilder.Entity<Project>()
            .HasIndex(x => x.Key)
            .IsUnique();

        // Issue
        modelBuilder.Entity<Issue>()
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById);

        modelBuilder.Entity<Issue>()
            .HasOne(x => x.LastUpdatedBy)
            .WithMany()
            .HasForeignKey(x => x.LastUpdatedById);

        modelBuilder.Entity<Issue>()
            .HasOne(x => x.Assignee)
            .WithMany()
            .HasForeignKey(x => x.AssigneeId);

        modelBuilder.Entity<Issue>()
            .HasOne(x => x.Project)
            .WithMany(p => p.Issues)
            .HasForeignKey(x => x.ProjectId);

        modelBuilder.Entity<Issue>()
            .HasMany(x => x.Comments)
            .WithOne(p => p.Issue)
            .HasForeignKey(x => x.IssueId);

        modelBuilder.Entity<Issue>()
            .Property(e => e.Type)
            .HasConversion<string>();

        // Comment
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById);

        // User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.AuthId)
            .IsUnique();

        // Label
        modelBuilder.Entity<Label>()
            .HasOne(l => l.Project)
            .WithMany()
            .HasForeignKey(l => l.ProjectId);

        modelBuilder.Entity<Label>()
            .HasOne(l => l.CreatedBy)
            .WithMany()
            .HasForeignKey(l => l.CreatedById);

        // Many-to-many relationship between Issue and Label
        modelBuilder.Entity<Issue>()
            .HasMany(i => i.Labels)
            .WithMany(l => l.Issues);
    }
}