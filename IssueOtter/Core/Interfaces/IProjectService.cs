using IssueOtter.Core.Dtos.Project;

namespace IssueOtter.Core.Interfaces;

public interface IProjectService
{
  Task<List<ProjectResponse>> GetAllProjectsAsync();
  Task<ProjectResponse?> GetProjectByKeyAsync(string key);
  Task<ProjectResponse?> CreateProjectAsync(CreateProjectRequest createProjectRequest, string userAuthId);
  Task<ProjectResponse?> DeleteProjectByKeyAsync(string key);
}
