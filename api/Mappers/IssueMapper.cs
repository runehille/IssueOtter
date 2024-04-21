using api.Dtos.Issue;
using api.Models;

namespace api.Mappers;

public static class IssueMapper
{
  public static IssueResponse MapIssueModelToIssueResponse(this IssueModel issueModel)
  {
    return new IssueResponse
    {
      Id = issueModel.Id,
      Key = issueModel.Key,
      Type = issueModel.Type,
      Title = issueModel.Title,
      Content = issueModel.Content,
      Status = issueModel.Status,
      Assignee = issueModel.Assignee?.MapUserModelToUserResponse(),
      CreatedBy = issueModel.CreatedBy?.MapUserModelToUserResponse(),
      CreatedOn = issueModel.CreatedOn.ToString("F"),
      LastUpdatedOn = issueModel.LastUpdatedOn.ToString("F"),
    };
  }

  public static IssueModel MapCreateIssueRequestToIssueModel(this CreateIssueRequest createIssueRequest)
  {
    return new IssueModel
    {
      Title = createIssueRequest.Title,
      Content = createIssueRequest.Content,
      Type = createIssueRequest.Type,
      Status = createIssueRequest.Status,
      AssigneeId = createIssueRequest.AssigneeId,
      CreatedById = createIssueRequest.CreatedById,
      LastUpdatedById = createIssueRequest.CreatedById,
      ProjectId = createIssueRequest.ProjectId
    };
  }
}