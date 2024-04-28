
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public enum IssueType
{
  Task,
  Bug
}

[Table("Issue")]
public class Issue
{
  public int Id { get; set; }
  public string Key { get; set; } = string.Empty;
  public IssueType Type { get; set; } = IssueType.Task;
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public int AssigneeId { get; set; }
  public User? Assignee { get; set; }
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int CreatedById { get; set; }
  public User? CreatedBy { get; set; }
  public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
  public int LastUpdatedById { get; set; }
  public User? LastUpdatedBy { get; set; }
  public int ProjectId { get; set; }
  public Project? Project { get; set; }
  public List<Comment> Comments { get; set; } = [];
  public bool IsDeleted { get; set; }
}