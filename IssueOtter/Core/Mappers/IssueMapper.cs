using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Mappers;

public static class IssueMapper
{
  // TODO Remove "Model" from function names
  public static IssueResponse MapIssueModelToIssueResponse(this Issue issueModel)
  {
    return new IssueResponse
    {
      Id = issueModel.Id,
      Key = issueModel.Key,
      Type = issueModel.Type,
      Title = issueModel.Title,
      Content = issueModel.Content,
      Status = issueModel.Status,
      Priority = issueModel.Priority,
      Assignee = issueModel.Assignee?.MapUserToUserResponse(),
      CreatedBy = issueModel.CreatedBy?.MapUserToUserResponse(),
      CreatedOn = issueModel.CreatedOn.ToString("F"),
      LastUpdatedOn = issueModel.LastUpdatedOn.ToString("F"),
      Labels = issueModel.Labels?.Select(l => l.MapLabelToLabelResponse()).ToList() ?? []
    };
  }

  public static Issue MapCreateIssueRequestToIssueModel(this CreateIssueRequest createIssueRequest)
  {
    return new Issue
    {
      Title = createIssueRequest.Title,
      Content = createIssueRequest.Content,
      Type = createIssueRequest.Type,
      Status = createIssueRequest.Status,
      Priority = createIssueRequest.Priority,
      AssigneeId = createIssueRequest.AssigneeId,
      CreatedById = createIssueRequest.CreatedById,
      LastUpdatedById = createIssueRequest.CreatedById,
      ProjectId = createIssueRequest.ProjectId
    };
  }

  public static Issue MapUpdateIssueRequestToIssueModel(this UpdateIssueRequest updateIssueRequest)
  {
    return new Issue
    {
      Title = updateIssueRequest.Title,
      Content = updateIssueRequest.Content,
      Type = updateIssueRequest.Type,
      Status = updateIssueRequest.Status,
      Priority = updateIssueRequest.Priority,
      AssigneeId = updateIssueRequest.AssigneeId
    };
  }
}