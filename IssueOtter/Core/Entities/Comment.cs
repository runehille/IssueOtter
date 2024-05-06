namespace IssueOtter.Core.Entities;

public class Comment
{
  public int Id { get; set; }
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int CreatedById { get; set; }
  public User? CreatedBy { get; set; }
  public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
  public bool IsEdited { get; set; }
  public bool IsDeleted { get; set; }
  public int IssueId { get; set; }
  public Issue? Issue { get; set; }
}