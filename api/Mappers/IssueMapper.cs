using api.Dtos.Issue;
using api.Models;

namespace api.Mappers;

public static class IssueMapper
{
    public static IssueResponse MapIssueModelToIssueResponse(this Issue issueModel)
    {
        return new IssueResponse
        {
            Id = issueModel.Id,
            Title = issueModel.Title,
            Content = issueModel.Content,
            CreatedOn = issueModel.CreatedOn,
            LastUpdatedOn = issueModel.LastUpdatedOn,
        };
    }

    public static Issue MapCreateIssueRequestToIssueModel(this CreateIssueRequest createIssueRequest)
    {
        return new Issue
        {
            Title = createIssueRequest.Title,
            Content = createIssueRequest.Content,
            CreatedById = createIssueRequest.CreatedById,
            ProjectId = createIssueRequest.ProjectId
        };
    }
}