using System.ComponentModel.DataAnnotations;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Dtos.Issue;

public class UpdateIssueStatusRequest
{
    [Required]
    [EnumDataType(typeof(IssueStatus))]
    public IssueStatus Status { get; set; }
}