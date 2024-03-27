using api.Dtos.Issue;
using api.Models;

namespace api.Mappers;

public static class IssueMapper
{
    public static IssueResponse MapIssueModelToIssueResponse(this IssueModel issueModel)
    {
        return new IssueResponse
        {
            Key = issueModel.Key,
            Title = issueModel.Title,
            Content = issueModel.Content,
            AssigneeId = issueModel.AssigneeId,
            CreatedOn = issueModel.CreatedOn,
            CreatedById = issueModel.CreatedById,
            LastUpdatedOn = issueModel.LastUpdatedOn,
            LastUpdatedById = issueModel.LastUpdatedById,
            Comments = issueModel.Comments.Select(x => x.MapCommentModelToCommentResponse()).ToList()
        };
    }

    public static IssueModel MapCreateIssueRequestToIssueModel(this CreateIssueRequest createIssueRequest)
    {
        return new IssueModel
        {
            Title = createIssueRequest.Title,
            Content = createIssueRequest.Content,
            AssigneeId = createIssueRequest.AssigneeId,
            CreatedById = createIssueRequest.CreatedById,
            LastUpdatedById = createIssueRequest.CreatedById,
            ProjectId = createIssueRequest.ProjectId
        };
    }
}