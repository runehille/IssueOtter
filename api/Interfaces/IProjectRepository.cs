using api.Models;

namespace api.Interfaces;

public interface IProjectRepository
{
    Task<List<ProjectModel>> GetAllAsync();
    Task<ProjectModel> GetByIdAsync(int id);
    Task<ProjectModel> GetByKeyAsync(string key);
    Task<ProjectModel> CreateAsync(ProjectModel projectModel);
    Task<ProjectModel?> UpdateIssueCountAsync(ProjectModel projectModel);

}