using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Dtos.Comment;

public class UpdateCommentRequest
{
    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
}