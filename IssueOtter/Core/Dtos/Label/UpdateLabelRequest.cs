using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Dtos.Label;

public class UpdateLabelRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be a valid hex color code")]
    public string Color { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
}