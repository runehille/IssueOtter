using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Interfaces;

public interface IProjectRepository
{
  Task<List<Project>> GetAllAsync();
  Task<Project> GetByIdAsync(int id);
  Task<Project> GetByKeyAsync(string key);
  Task<Project> CreateAsync(Project Project);
  Task<Project?> DeleteByKeyAsync(string key);
  Task<Project?> UpdateIssueCountAsync(Project Project);

}