

using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Issue
{
  public class CreateIssueRequest
  {
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
    [Required]
    public int AssigneeId { get; set; }
    [Required]
    public int CreatedById { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public string ProjectKey { get; set; } = string.Empty;
  }
}