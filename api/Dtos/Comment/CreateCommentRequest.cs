namespace api.Dtos.Comment;

public class CreateCommentRequest
{
  public string Content { get; set; } = string.Empty;
  public int IssueId { get; set; }
}
