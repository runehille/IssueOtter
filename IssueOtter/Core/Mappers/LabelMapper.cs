using IssueOtter.Core.Dtos.Label;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Mappers;

public static class LabelMapper
{
    public static LabelResponse MapLabelToLabelResponse(this Label label)
    {
        return new LabelResponse
        {
            Id = label.Id,
            Name = label.Name,
            Color = label.Color,
            Description = label.Description,
            ProjectId = label.ProjectId,
            CreatedOn = label.CreatedOn.ToString("F")
        };
    }

    public static Label MapCreateLabelRequestToLabel(this CreateLabelRequest createLabelRequest)
    {
        return new Label
        {
            Name = createLabelRequest.Name,
            Color = createLabelRequest.Color,
            Description = createLabelRequest.Description,
            ProjectId = createLabelRequest.ProjectId
        };
    }

    public static Label MapUpdateLabelRequestToLabel(this UpdateLabelRequest updateLabelRequest)
    {
        return new Label
        {
            Name = updateLabelRequest.Name,
            Color = updateLabelRequest.Color,
            Description = updateLabelRequest.Description
        };
    }
}