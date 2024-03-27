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
}