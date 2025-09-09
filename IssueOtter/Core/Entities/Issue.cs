using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Entities;

public enum IssueType
{
    Task,
    Bug
}

public enum IssueStatus
{
    ToDo,
    InProgress,
    InReview,
    Done,
    Closed
}

public enum IssuePriority
{
    Low,
    Medium,
    High,
    Critical
}

public class Issue
{
    public int Id { get; init; }

    [MaxLength(10)] public string Key { get; set; } = string.Empty;

    public IssueType Type { get; set; } = IssueType.Task;

    [MaxLength(100)] public string Title { get; set; } = string.Empty;

    [MaxLength(500)] public string Content { get; set; } = string.Empty;

    public IssueStatus Status { get; set; } = IssueStatus.ToDo;

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public int? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public DateTime CreatedOn { get; init; } = DateTime.Now;
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; init; }
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    public int? LastUpdatedById { get; set; }
    public User? LastUpdatedBy { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    public List<Comment> Comments { get; set; } = [];

    // Many-to-many relationship with Labels
    public List<Label> Labels { get; set; } = [];

    public bool IsDeleted { get; set; }
}