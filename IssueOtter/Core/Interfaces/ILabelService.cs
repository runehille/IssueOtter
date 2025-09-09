using IssueOtter.Core.Dtos.Label;

namespace IssueOtter.Core.Interfaces;

public interface ILabelService
{
    Task<List<LabelResponse>> GetAllLabelsAsync();
    Task<List<LabelResponse>> GetLabelsByProjectIdAsync(int projectId);
    Task<LabelResponse?> GetLabelByIdAsync(int id);
    Task<LabelResponse?> CreateLabelAsync(CreateLabelRequest createLabelRequest, string userAuthId);
    Task<LabelResponse?> UpdateLabelAsync(int id, UpdateLabelRequest updateLabelRequest);
    Task<LabelResponse?> DeleteLabelAsync(int id);
}