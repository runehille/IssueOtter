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

  public async Task<ProjectModel> CreateAsync(ProjectModel projectModel)
  {
    await _context.Project.AddAsync(projectModel);
    await _context.SaveChangesAsync();

    return projectModel;
  }

  public async Task<ProjectModel?> DeleteByKeyAsync(string key)
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

  public async Task<List<ProjectModel>> GetAllAsync()
  {
    var projects = await _context.Project.ToListAsync();
    return projects;
  }

  public async Task<ProjectModel> GetByIdAsync(int projectId)
  {

    var project = await _context.Project.FirstOrDefaultAsync(x => x.Id == projectId);

    return project!;
  }

  public async Task<ProjectModel> GetByKeyAsync(string key)
  {
    var project = await _context.Project.FirstOrDefaultAsync(x => x.Key == key);

    return project!;
  }

  public async Task<ProjectModel?> UpdateIssueCountAsync(ProjectModel projectModel)
  {
    var existingProject = await _context.Project.FindAsync(projectModel.Id);

    if (existingProject is null)
    {
      return null;
    }

    existingProject.IssueCount = projectModel.IssueCount;

    await _context.SaveChangesAsync();

    return existingProject;
  }
}