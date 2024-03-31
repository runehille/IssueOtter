using api.Dtos.Project;
using api.Models;

namespace api.Mappers;

public static class ProjectMapper
{
    public static ProjectResponse MapProjectModelToProjectResponse(this ProjectModel projectModel)
    {
        return new ProjectResponse
        {
            Key = projectModel.Key,
            Title = projectModel.Title,
            Description = projectModel.Description,
        };
    }

    public static ProjectModel MapCreateProjectRequestToProjectModel(this CreateProjectRequest createProjectRequest)
    {
        return new ProjectModel
        {
            Key = createProjectRequest.Key,
            Title = createProjectRequest.Title,
            Description = createProjectRequest.Description,
        };
    }
}