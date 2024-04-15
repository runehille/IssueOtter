namespace api.Dtos.Comment;

public class CommentResponse
{
  public int Id { get; set; }
  public string? Content { get; set; }
  public string? CreatedOn { get; set; }
  public int CreatedById { get; set; }
  public int IssueId { get; set; }
}