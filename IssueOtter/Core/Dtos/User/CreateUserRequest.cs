using System.ComponentModel.DataAnnotations;

namespace IssueOtter.Core.Dtos.User;

public class CreateUserRequest
{
  [Required]
  public string? Email { get; set; }
  [Required]
  public string? FirstName { get; set; }
  [Required]
  public string? LastName { get; set; }
}