using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(50)] public string? AuthId { get; set; }
    [MaxLength(50)] public string? Email { get; set; }
    [MaxLength(50)] public string? FirstName { get; set; }
    [MaxLength(50)] public string? LastName { get; set; }
}