using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Dtos.Label;
using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using IssueOtter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Services;

public class ServiceLevelDataSeedingService(
    IUserService userService,
    IProjectService projectService,
    IIssueService issueService,
    ICommentService commentService,
    ILabelService labelService,
    ApplicationDbContext context,
    ILogger<ServiceLevelDataSeedingService> logger)
{
    public async Task SeedDataAsync()
    {
        try
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.User.AnyAsync())
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
                return;
            }

            logger.LogInformation("Starting service-level data seeding...");

            await SeedUsersAsync();
            await SeedProjectsAsync();
            await SeedLabelsAsync();
            await SeedIssuesAsync();
            await SeedCommentsAsync();

            logger.LogInformation("Service-level data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding data at service level.");
            throw;
        }
    }

    private async Task SeedUsersAsync()
    {
        var users = new[]
        {
            new { AuthId = "dev|1234", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new { AuthId = "dev-user-2", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
            new { AuthId = "dev-user-3", FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com" }
        };

        foreach (var userData in users)
        {
            var createUserRequest = new CreateUserRequest
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email
            };

            await userService.CreateUserAsync(createUserRequest, userData.AuthId);
        }

        logger.LogInformation("Seeded {UsersCount} users using UserService.", users.Length);
    }

    private async Task SeedProjectsAsync()
    {
        var projects = new[]
        {
            new { Key = "DEMO", Title = "Demo Project", Description = "A demonstration project for issue tracking", AuthId = "dev|1234" },
            new { Key = "TEST", Title = "Test Project", Description = "A test project for development purposes", AuthId = "dev-user-2" }
        };

        foreach (var projectData in projects)
        {
            var createProjectRequest = new CreateProjectRequest
            {
                Key = projectData.Key,
                Title = projectData.Title,
                Description = projectData.Description
            };

            await projectService.CreateProjectAsync(createProjectRequest, projectData.AuthId);
        }

        logger.LogInformation("Seeded {ProjectsCount} projects using ProjectService.", projects.Length);
    }

    private async Task SeedLabelsAsync()
    {
        // Get projects to assign labels to
        var demoProject = await context.Project.FirstOrDefaultAsync(p => p.Key == "DEMO");
        var testProject = await context.Project.FirstOrDefaultAsync(p => p.Key == "TEST");
        
        if (demoProject == null || testProject == null)
        {
            logger.LogWarning("Projects not found for label seeding.");
            return;
        }

        var labels = new[]
        {
            // DEMO project labels
            new { Name = "bug", Color = "#dc2626", Description = "Something isn't working", ProjectId = demoProject.Id, AuthId = "dev|1234" },
            new { Name = "enhancement", Color = "#059669", Description = "New feature or request", ProjectId = demoProject.Id, AuthId = "dev|1234" },
            new { Name = "question", Color = "#7c3aed", Description = "Further information is requested", ProjectId = demoProject.Id, AuthId = "dev|1234" },
            new { Name = "documentation", Color = "#0891b2", Description = "Improvements or additions to documentation", ProjectId = demoProject.Id, AuthId = "dev-user-2" },
            new { Name = "good first issue", Color = "#22c55e", Description = "Good for newcomers", ProjectId = demoProject.Id, AuthId = "dev-user-2" },
            // TEST project labels  
            new { Name = "testing", Color = "#f59e0b", Description = "Related to testing functionality", ProjectId = testProject.Id, AuthId = "dev-user-2" },
            new { Name = "high priority", Color = "#dc2626", Description = "Needs immediate attention", ProjectId = testProject.Id, AuthId = "dev-user-2" },
            new { Name = "refactor", Color = "#6b7280", Description = "Code refactoring needed", ProjectId = testProject.Id, AuthId = "dev-user-3" }
        };

        foreach (var labelData in labels)
        {
            var createLabelRequest = new CreateLabelRequest
            {
                Name = labelData.Name,
                Color = labelData.Color,
                Description = labelData.Description,
                ProjectId = labelData.ProjectId
            };

            await labelService.CreateLabelAsync(createLabelRequest, labelData.AuthId);
        }

        logger.LogInformation("Seeded {LabelsCount} labels using LabelService.", labels.Length);
    }

    private async Task SeedIssuesAsync()
    {
        var issues = new[]
        {
            new { Key = "DEMO-1", Title = "Setup authentication system", Content = "Implement Auth0 authentication for the application", Type = IssueType.Task, Status = IssueStatus.ToDo, Priority = IssuePriority.High, ProjectKey = "DEMO", AuthId = "dev|1234" },
            new { Key = "DEMO-2", Title = "Fix login redirect issue", Content = "Users are not being redirected properly after login", Type = IssueType.Bug, Status = IssueStatus.InProgress, Priority = IssuePriority.Critical, ProjectKey = "DEMO", AuthId = "dev|1234" },
            new { Key = "DEMO-3", Title = "Create user dashboard", Content = "Build a dashboard showing user's assigned issues and project overview", Type = IssueType.Task, Status = IssueStatus.Done, Priority = IssuePriority.Medium, ProjectKey = "DEMO", AuthId = "dev-user-2" },
            new { Key = "DEMO-4", Title = "Database connection timeout", Content = "API calls are timing out when connecting to the database", Type = IssueType.Bug, Status = IssueStatus.ToDo, Priority = IssuePriority.Critical, ProjectKey = "DEMO", AuthId = "dev-user-3" },
            new { Key = "DEMO-5", Title = "Implement comment system", Content = "Allow users to comment on issues for collaboration", Type = IssueType.Task, Status = IssueStatus.InProgress, Priority = IssuePriority.Medium, ProjectKey = "DEMO", AuthId = "dev|1234" },
            new { Key = "TEST-1", Title = "Write unit tests", Content = "Create comprehensive unit tests for the API controllers", Type = IssueType.Task, Status = IssueStatus.ToDo, Priority = IssuePriority.Low, ProjectKey = "TEST", AuthId = "dev-user-2" },
            new { Key = "TEST-2", Title = "Swagger documentation missing", Content = "Some API endpoints are not properly documented in Swagger", Type = IssueType.Bug, Status = IssueStatus.Done, Priority = IssuePriority.Medium, ProjectKey = "TEST", AuthId = "dev-user-2" },
            new { Key = "TEST-3", Title = "Setup CI/CD pipeline", Content = "Configure automated testing and deployment pipeline", Type = IssueType.Task, Status = IssueStatus.InProgress, Priority = IssuePriority.High, ProjectKey = "TEST", AuthId = "dev-user-3" }
        };

        foreach (var issueData in issues)
        {
            var createIssueRequest = new CreateIssueRequest
            {
                Title = issueData.Title,
                Content = issueData.Content,
                Type = issueData.Type,
                Status = issueData.Status,
                Priority = issueData.Priority,
                ProjectKey = issueData.ProjectKey
            };

            await issueService.CreateIssueAsync(createIssueRequest, issueData.AuthId);
        }

        logger.LogInformation("Seeded {IssuesCount} issues using IssueService.", issues.Length);
    }

    private async Task SeedCommentsAsync()
    {
        var comments = new[]
        {
            new { Content = "I've started working on the Auth0 integration. Should be completed by end of week.", IssueKey = "DEMO-1", AuthId = "dev|1234" },
            new { Content = "Found the issue - it's related to the callback URL configuration. Working on a fix.", IssueKey = "DEMO-2", AuthId = "dev-user-2" },
            new { Content = "Dashboard is now live! Users can see their assigned issues and project statistics.", IssueKey = "DEMO-3", AuthId = "dev-user-3" },
            new { Content = "This might be related to the connection pool settings. Let me investigate.", IssueKey = "DEMO-4", AuthId = "dev|1234" },
            new { Content = "Comments can now be added and retrieved. Working on edit/delete functionality next.", IssueKey = "DEMO-5", AuthId = "dev-user-2" }
        };

        foreach (var commentData in comments)
        {
            var createCommentRequest = new CreateCommentRequest
            {
                Content = commentData.Content,
                IssueKey = commentData.IssueKey
            };

            await commentService.CreateCommentAsync(createCommentRequest, commentData.AuthId);
        }

        logger.LogInformation("Seeded {CommentsCount} comments using CommentService.", comments.Length);
    }
}