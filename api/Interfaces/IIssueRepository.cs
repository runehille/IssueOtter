using api.Models;

namespace api.Interfaces;

public interface IIssueRepository
{
    Task<List<Issue>> GetAllAsync();
    Task<Issue?> GetByIdAsync(int id);
    Task<Issue?> CreateAsync(Issue issueModel);
    Task<Issue?> UpdateAsync(int id, Issue issueModel);
    Task<Issue?> DeleteAsync(int id);
}