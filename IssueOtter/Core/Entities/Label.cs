using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Entities;

public class Label
{
    public int Id { get; init; }

    [MaxLength(50)] public string Name { get; set; } = string.Empty;

    [MaxLength(7)] public string Color { get; set; } = "#6B7280"; // Default gray color

    [MaxLength(200)] public string Description { get; set; } = string.Empty;

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public int CreatedById { get; set; }
    public User? CreatedBy { get; init; }

    // Many-to-many relationship with Issues
    public List<Issue> Issues { get; set; } = [];
}