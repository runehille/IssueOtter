using System.ComponentModel.DataAnnotations.Schema;

namespace IssueOtter.Core.Entities;

[Table("Project")]
public class Project
{
  public int Id { get; set; }
  public string Key { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public int AdminId { get; set; }
  public User? Admin { get; set; }
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int CreatedById { get; set; }
  public User? CreatedBy { get; set; }
  public List<Issue> Issues { get; set; } = [];
  public int IssueCount { get; set; } = 0;
  public bool IsDeleted { get; set; }
}