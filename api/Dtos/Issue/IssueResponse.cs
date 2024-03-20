using api.Models;

namespace api.Dtos.Issue;

public class IssueResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int CreatedByUserId { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    public int LastUpdatedByUserId { get; set; }
    public User? LastUpdatedBy { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}