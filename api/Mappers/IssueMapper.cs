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
            Created = issueModel.Created,
            LastUpdated = issueModel.LastUpdated
        };
    }
}