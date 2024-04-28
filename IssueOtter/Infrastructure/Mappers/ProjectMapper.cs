using api.Dtos.Project;
using api.Models;

namespace api.Mappers;

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