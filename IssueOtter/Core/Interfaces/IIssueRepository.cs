using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Interfaces;

public interface IIssueRepository
{
  Task<List<Issue>> GetAllAsync();
  Task<List<Issue>> GetAllByProjectIdAsync(int id);
  Task<List<Issue>> GetAllByProjectKeyAsync(string key);
  Task<Issue?> GetByIdAsync(int id);
  Task<Issue?> GetByKeyAsync(string key);
  Task<Issue?> CreateAsync(Issue Issue);
  Task<Issue?> UpdateAsync(int id, Issue Issue);
  Task<Issue?> UpdateStatusAsync(int id, IssueStatus status, int updatedById);
  Task<Issue?> UpdateStatusByKeyAsync(string key, IssueStatus status, int updatedById);
  Task<Issue?> DeleteAsync(int id);
  Task<Issue?> DeleteByKeyAsync(string key);
}