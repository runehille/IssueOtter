using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Dtos.Project;

public class UpdateProjectRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}