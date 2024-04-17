using api.Dtos.Comment;
using api.Dtos.User;
using api.Models;

namespace api.Dtos.Issue;

public class IssueResponse
{
  public int Id { get; set; }
  public string Key { get; set; } = string.Empty;
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
  public bool IsDeleted { get; set; }
}