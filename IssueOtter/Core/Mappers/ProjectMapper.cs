using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Mappers;

public static class ProjectMapper
{
  public static ProjectResponse MapProjectToProjectResponse(this Project Project)
  {
    return new ProjectResponse
    {
      Key = Project.Key,
      Title = Project.Title,
      Description = Project.Description,
    };
  }

  public static Project MapCreateProjectRequestToProject(this CreateProjectRequest createProjectRequest)
  {
    return new Project
    {
      Key = createProjectRequest.Key,
      Title = createProjectRequest.Title,
      Description = createProjectRequest.Description,
    };
  }
}