using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.Project;

public class ProjectService(IProjectRepository projectRepository, IUserRepository userRepository)
    : IProjectService
{
    public async Task<ProjectResponse?> CreateProjectAsync(CreateProjectRequest createProjectRequest, string userAuthId)
    {
        var projectToCreate = createProjectRequest.MapCreateProjectRequestToProject();

        var user = await userRepository.GetByAuthId(userAuthId);

        if (user is not null)
        {
            projectToCreate.AdminId = user.Id;
            projectToCreate.CreatedById = user.Id;
        }

        await projectRepository.CreateAsync(projectToCreate);

        var project = await projectRepository.GetByIdAsync(projectToCreate.Id);
        return project?.MapProjectToProjectResponse();
    }

    public async Task<ProjectResponse?> DeleteProjectByKeyAsync(string key)
    {
        var project = await projectRepository.DeleteByKeyAsync(key);

        return project?.MapProjectToProjectResponse();
    }

    public async Task<List<ProjectResponse>> GetAllProjectsAsync()
    {
        var projects = await projectRepository.GetAllAsync();

        var projectsResponse = projects.Select(x => x.MapProjectToProjectResponse()).ToList();

        return projectsResponse;
    }

    public async Task<ProjectResponse?> GetProjectByKeyAsync(string key)
    {
        var project = await projectRepository.GetByKeyAsync(key);

        return project?.MapProjectToProjectResponse();
    }

    public async Task<ProjectResponse?> UpdateProjectAsync(string key, UpdateProjectRequest updateProjectRequest)
    {
        var projectToUpdate = updateProjectRequest.MapUpdateProjectRequestToProject();
        var updatedProject = await projectRepository.UpdateAsync(key, projectToUpdate);

        return updatedProject?.MapProjectToProjectResponse();
    }
}