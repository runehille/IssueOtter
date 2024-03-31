using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {

  }

  public DbSet<ProjectModel> Project { get; set; }
  public DbSet<IssueModel> Issue { get; set; }
  public DbSet<CommentModel> Comment { get; set; }
  public DbSet<UserModel> User { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Project
    modelBuilder.Entity<ProjectModel>()
    .HasMany(x => x.Issues)
    .WithOne(p => p.Project)
    .HasForeignKey(x => x.ProjectId);

    modelBuilder.Entity<ProjectModel>()
    .HasIndex(x => x.Key)
    .IsUnique();

    // Issue
    modelBuilder.Entity<IssueModel>()
        .HasOne(x => x.CreatedBy)
        .WithMany()
        .HasForeignKey(x => x.CreatedById);

    modelBuilder.Entity<IssueModel>()
        .HasOne(x => x.LastUpdatedBy)
        .WithMany()
        .HasForeignKey(x => x.LastUpdatedById);

    modelBuilder.Entity<IssueModel>()
        .HasOne(x => x.Assignee)
        .WithMany()
        .HasForeignKey(x => x.AssigneeId);

    modelBuilder.Entity<IssueModel>()
        .HasOne(x => x.Project)
        .WithMany(p => p.Issues)
        .HasForeignKey(x => x.ProjectId);

    modelBuilder.Entity<IssueModel>()
        .HasMany(x => x.Comments)
        .WithOne(p => p.Issue)
        .HasForeignKey(x => x.IssueId);

    // User
    modelBuilder.Entity<UserModel>()
        .HasIndex(u => u.AuthId)
        .IsUnique();
  }
}