using api.Dtos.Comment;
using api.Models;

namespace api.Dtos.Issue;

public class IssueResponse
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int AssigneeId { get; set; }
    public UserModel? Assignee { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int CreatedById { get; set; }
    public UserModel? CreatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    public int LastUpdatedById { get; set; }
    public UserModel? LastUpdatedBy { get; set; }
    public int ProjectId { get; set; }
    public List<CommentResponse> Comments { get; set; } = [];
    public bool IsDeleted { get; set; }
}