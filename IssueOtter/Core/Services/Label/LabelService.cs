using IssueOtter.Core.Dtos.Label;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.Label;

public class LabelService(ILabelRepository labelRepository, IUserRepository userRepository) : ILabelService
{
    public async Task<List<LabelResponse>> GetAllLabelsAsync()
    {
        var labels = await labelRepository.GetAllAsync();
        return labels.Select(l => l.MapLabelToLabelResponse()).ToList();
    }

    public async Task<List<LabelResponse>> GetLabelsByProjectIdAsync(int projectId)
    {
        var labels = await labelRepository.GetAllByProjectIdAsync(projectId);
        return labels.Select(l => l.MapLabelToLabelResponse()).ToList();
    }

    public async Task<LabelResponse?> GetLabelByIdAsync(int id)
    {
        var label = await labelRepository.GetByIdAsync(id);
        return label?.MapLabelToLabelResponse();
    }

    public async Task<LabelResponse?> CreateLabelAsync(CreateLabelRequest createLabelRequest, string userAuthId)
    {
        var user = await userRepository.GetByAuthId(userAuthId);
        if (user is null) return null;

        var labelToCreate = createLabelRequest.MapCreateLabelRequestToLabel();
        labelToCreate.CreatedById = user.Id;

        await labelRepository.CreateAsync(labelToCreate);

        var createdLabel = await labelRepository.GetByIdAsync(labelToCreate.Id);
        return createdLabel?.MapLabelToLabelResponse();
    }

    public async Task<LabelResponse?> UpdateLabelAsync(int id, UpdateLabelRequest updateLabelRequest)
    {
        var labelToUpdate = updateLabelRequest.MapUpdateLabelRequestToLabel();
        var updatedLabel = await labelRepository.UpdateAsync(id, labelToUpdate);

        return updatedLabel?.MapLabelToLabelResponse();
    }

    public async Task<LabelResponse?> DeleteLabelAsync(int id)
    {
        var deletedLabel = await labelRepository.DeleteAsync(id);
        return deletedLabel?.MapLabelToLabelResponse();
    }
}