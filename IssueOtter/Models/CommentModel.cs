using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Comment")]
public class CommentModel
{
  public int Id { get; set; }
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int CreatedById { get; set; }
  public UserModel? CreatedBy { get; set; }
  public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
  public bool IsEdited { get; set; }
  public bool IsDeleted { get; set; }
  public int IssueId { get; set; }
  public IssueModel? Issue { get; set; }
}