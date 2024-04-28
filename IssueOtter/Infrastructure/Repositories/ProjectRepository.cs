using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class ProjectRepository : IProjectRepository
{
  private readonly ApplicationDbContext _context;

  public ProjectRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Project> CreateAsync(Project Project)
  {
    await _context.Project.AddAsync(Project);
    await _context.SaveChangesAsync();

    return Project;
  }

  public async Task<Project?> DeleteByKeyAsync(string key)
  {
    var project = await _context.Project.FirstOrDefaultAsync(x => x.Key == key);

    if (project is null)
    {
      return project;
    }

    _context.Project.Remove(project);
    await _context.SaveChangesAsync();

    return project;
  }

  public async Task<List<Project>> GetAllAsync()
  {
    var projects = await _context.Project.ToListAsync();
    return projects;
  }

  public async Task<Project> GetByIdAsync(int projectId)
  {

    var project = await _context.Project.FirstOrDefaultAsync(x => x.Id == projectId);

    return project!;
  }

  public async Task<Project> GetByKeyAsync(string key)
  {
    var project = await _context.Project.FirstOrDefaultAsync(x => x.Key == key);

    return project!;
  }

  public async Task<Project?> UpdateIssueCountAsync(Project Project)
  {
    var existingProject = await _context.Project.FindAsync(Project.Id);

    if (existingProject is null)
    {
      return null;
    }

    existingProject.IssueCount = Project.IssueCount;

    await _context.SaveChangesAsync();

    return existingProject;
  }
}