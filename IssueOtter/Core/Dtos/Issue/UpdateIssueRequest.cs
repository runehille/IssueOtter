using System.ComponentModel.DataAnnotations;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Dtos.Issue;

public class UpdateIssueRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    [EnumDataType(typeof(IssueType))]
    public IssueType Type { get; set; }
    
    [EnumDataType(typeof(IssueStatus))]
    public IssueStatus Status { get; set; }
    
    [EnumDataType(typeof(IssuePriority))]
    public IssuePriority Priority { get; set; }
    [Required]
    public int AssigneeId { get; set; }
}