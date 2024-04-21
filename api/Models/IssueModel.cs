
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public enum IssueType
{
  Task,
  Bug
}

[Table("Issue")]
public class IssueModel
{
  public int Id { get; set; }
  public string Key { get; set; } = string.Empty;
  public IssueType Type { get; set; } = IssueType.Task;
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public int AssigneeId { get; set; }
  public UserModel? Assignee { get; set; }
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int CreatedById { get; set; }
  public UserModel? CreatedBy { get; set; }
  public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
  public int LastUpdatedById { get; set; }
  public UserModel? LastUpdatedBy { get; set; }
  public int ProjectId { get; set; }
  public ProjectModel? Project { get; set; }
  public List<CommentModel> Comments { get; set; } = [];
  public bool IsDeleted { get; set; }
}