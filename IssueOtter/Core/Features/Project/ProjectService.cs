using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace IssueOtter.Core.Features;

public class ProjectService : IProjectService
{
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;

  public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository)
  {
    _projectRepository = projectRepository;
    _userRepository = userRepository;
  }

  public async Task<ProjectResponse?> CreateProjectAsync(CreateProjectRequest createProjectRequest, string userAuthId)
  {
    var projectToCreate = createProjectRequest.MapCreateProjectRequestToProject();

    var user = await _userRepository.GetByAuthId(userAuthId);

    if (user is not null)
    {
      projectToCreate.AdminId = user.Id;
      projectToCreate.CreatedById = user.Id;
    }
    await _projectRepository.CreateAsync(projectToCreate);

    var project = await _projectRepository.GetByIdAsync(projectToCreate.Id);
    return project.MapProjectToProjectResponse();

  }

  public async Task<List<ProjectResponse>> GetAllProjectsAsync()
  {
    var projects = await _projectRepository.GetAllAsync();

    var projectsResponse = projects.Select(x => x.MapProjectToProjectResponse()).ToList();

    return projectsResponse;
  }

  public async Task<ProjectResponse?> GetProjectByKeyAsync(string key)
  {
    var project = await _projectRepository.GetByKeyAsync(key);

    if (project is null)
    {
      return null;
    }

    var projectResponse = project.MapProjectToProjectResponse();

    return projectResponse;
  }
}
