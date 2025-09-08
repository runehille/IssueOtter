using IssueOtter.Core.Entities;
using IssueOtter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Infrastructure.Repositories;

public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
{
    public async Task<Project> CreateAsync(Project project)
    {
        await context.Project.AddAsync(project);
        await context.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> DeleteByKeyAsync(string key)
    {
        var project = await context.Project.FirstOrDefaultAsync(x => x.Key == key);

        if (project is null) return project;

        context.Project.Remove(project);
        await context.SaveChangesAsync();

        return project;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        var projects = await context.Project.ToListAsync();
        return projects;
    }

    public async Task<Project> GetByIdAsync(int projectId)
    {
        var project = await context.Project.FirstOrDefaultAsync(x => x.Id == projectId);

        return project!;
    }

    public async Task<Project> GetByKeyAsync(string key)
    {
        var project = await context.Project.FirstOrDefaultAsync(x => x.Key == key);

        return project!;
    }

    public async Task<Project?> UpdateAsync(string key, Project project)
    {
        var existingProject = await context.Project.FirstOrDefaultAsync(x => x.Key == key);
        if (existingProject == null) return null;

        try
        {
            existingProject.Title = project.Title;
            existingProject.Description = project.Description;

            context.Project.Update(existingProject);
            await context.SaveChangesAsync();

            return existingProject;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }

    public async Task<Project?> UpdateIssueCountAsync(Project project)
    {
        var existingProject = await context.Project.FindAsync(project.Id);

        if (existingProject is null) return null;

        existingProject.IssueCount = project.IssueCount;

        await context.SaveChangesAsync();

        return existingProject;
    }
}