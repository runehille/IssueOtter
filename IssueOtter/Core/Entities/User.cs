using System.ComponentModel.DataAnnotations.Schema;

namespace IssueOtter.Core.Entities;

[Table("User")]
public class User
{
  public int Id { get; set; }
  public string? AuthId { get; set; }
  public string? Email { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
}