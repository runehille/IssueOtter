using System.Text.Json;
using IssueOtter.Core.Entities;
using IssueOtter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Services;

public class DataSeedingService(ApplicationDbContext context, ILogger<DataSeedingService> logger)
{
    public async Task SeedDataAsync()
    {
        try
        {
            // Ensure the database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.User.AnyAsync())
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
                return;
            }

            logger.LogInformation("Starting data seeding...");

            // Seed Users
            await SeedUsersAsync();
            
            // Seed Projects
            await SeedProjectsAsync();
            
            // Seed Issues
            await SeedIssuesAsync();
            
            // Seed Comments
            await SeedCommentsAsync();

            await context.SaveChangesAsync();
            logger.LogInformation("Data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding data.");
            throw;
        }
    }

    private async Task SeedUsersAsync()
    {
        var json = await File.ReadAllTextAsync("SeedData/users.json");
        var users = JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (users != null)
        {
            await context.User.AddRangeAsync(users);
            logger.LogInformation("Seeded {UsersCount} users.", users.Count);
        }
    }

    private async Task SeedProjectsAsync()
    {
        var json = await File.ReadAllTextAsync("SeedData/projects.json");
        var projects = JsonSerializer.Deserialize<List<Project>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (projects != null)
        {
            await context.Project.AddRangeAsync(projects);
            logger.LogInformation("Seeded {ProjectsCount} projects.", projects.Count);
        }
    }

    private async Task SeedIssuesAsync()
    {
        var json = await File.ReadAllTextAsync("SeedData/issues.json");
        var issues = JsonSerializer.Deserialize<List<Issue>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (issues != null)
        {
            await context.Issue.AddRangeAsync(issues);
            logger.LogInformation("Seeded {IssuesCount} issues.", issues.Count);
        }
    }

    private async Task SeedCommentsAsync()
    {
        var json = await File.ReadAllTextAsync("SeedData/comments.json");
        var comments = JsonSerializer.Deserialize<List<Comment>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (comments != null)
        {
            await context.Comment.AddRangeAsync(comments);
            logger.LogInformation("Seeded {CommentsCount} comments.", comments.Count);
        }
    }
}