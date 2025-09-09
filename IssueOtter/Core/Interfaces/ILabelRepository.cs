using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Interfaces;

public interface ILabelRepository
{
    Task<List<Label>> GetAllAsync();
    Task<List<Label>> GetAllByProjectIdAsync(int projectId);
    Task<Label?> GetByIdAsync(int id);
    Task<List<Label>> GetByIdsAsync(List<int> ids);
    Task CreateAsync(Label label);
    Task<Label?> UpdateAsync(int id, Label label);
    Task<Label?> DeleteAsync(int id);
}