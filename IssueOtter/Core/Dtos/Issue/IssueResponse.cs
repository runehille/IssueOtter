using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Dtos.Issue;

public class IssueResponse
{
  public int Id { get; set; }
  public string Key { get; set; } = string.Empty;
  public IssueType Type { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public int AssigneeId { get; set; }
  public UserResponse? Assignee { get; set; }
  public string CreatedOn { get; set; } = string.Empty;
  public int CreatedById { get; set; }
  public UserResponse? CreatedBy { get; set; }
  public string LastUpdatedOn { get; set; } = string.Empty;
  public int ProjectId { get; set; }
  public List<CommentResponse> Comments { get; set; } = [];

  // TODO Remove IsDeleted
  public bool IsDeleted { get; set; }
}