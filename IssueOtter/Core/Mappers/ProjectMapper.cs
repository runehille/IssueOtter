using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Mappers;

public static class ProjectMapper
{
    public static ProjectResponse MapProjectToProjectResponse(this Project project)
    {
        return new ProjectResponse
        {
            Key = project.Key,
            Title = project.Title,
            Description = project.Description
        };
    }

    public static Project MapCreateProjectRequestToProject(this CreateProjectRequest createProjectRequest)
    {
        return new Project
        {
            Key = createProjectRequest.Key,
            Title = createProjectRequest.Title,
            Description = createProjectRequest.Description
        };
    }

    public static Project MapUpdateProjectRequestToProject(this UpdateProjectRequest updateProjectRequest)
    {
        return new Project
        {
            Title = updateProjectRequest.Title,
            Description = updateProjectRequest.Description
        };
    }
}