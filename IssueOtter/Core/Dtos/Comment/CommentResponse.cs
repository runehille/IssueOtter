using IssueOtter.Core.Dtos.User;

namespace IssueOtter.Core.Dtos.Comment;

public class CommentResponse
{
  public int Id { get; set; }
  public string? Content { get; set; }
  public string? CreatedOn { get; set; }
  public int CreatedById { get; set; }
  public UserResponse? CreatedBy { get; set; }
  public int IssueId { get; set; }
}