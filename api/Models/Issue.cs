
namespace api.Models;

public class Issue
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
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