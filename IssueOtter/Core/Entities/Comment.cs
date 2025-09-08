using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Entities;

public class Comment
{
    public int Id { get; init; }
    [MaxLength(500)] public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; init; } = DateTime.Now;
    public int CreatedById { get; set; }
    public User? CreatedBy { get; init; }
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }
    public int IssueId { get; set; }
    public Issue? Issue { get; init; }
}