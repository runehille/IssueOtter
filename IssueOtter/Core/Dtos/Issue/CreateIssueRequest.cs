using System.ComponentModel.DataAnnotations;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Dtos.Issue;

public class CreateIssueRequest
{
  [Required]
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;

  [EnumDataType(typeof(IssueType))]
  public IssueType Type { get; set; }
  public string Status { get; set; } = string.Empty;
  [Required]
  public int AssigneeId { get; set; }
  [Required]
  public int CreatedById { get; set; }
  [Required]
  public int ProjectId { get; set; }
  [Required]
  public string ProjectKey { get; set; } = string.Empty;
}