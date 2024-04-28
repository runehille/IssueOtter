using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Project;

public class CreateProjectRequest
{
    [Required]
    public string Key { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}