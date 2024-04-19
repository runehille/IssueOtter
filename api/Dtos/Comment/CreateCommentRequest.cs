namespace api.Dtos.Comment;

public class CreateCommentRequest
{
  public string Content { get; set; } = string.Empty;
  public string IssueKey { get; set; } = string.Empty;
}
