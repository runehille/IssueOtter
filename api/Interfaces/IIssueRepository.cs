using api.Models;

namespace api.Interfaces;

public interface IIssueRepository
{
  Task<List<IssueModel>> GetAllAsync();
  Task<List<IssueModel>> GetAllByProjectIdAsync(int id);
  Task<List<IssueModel>> GetAllByProjectKeyAsync(string key);
  Task<IssueModel?> GetByIdAsync(int id);
  Task<IssueModel?> CreateAsync(IssueModel issueModel);
  Task<IssueModel?> UpdateAsync(int id, IssueModel issueModel);
  Task<IssueModel?> DeleteAsync(int id);
}