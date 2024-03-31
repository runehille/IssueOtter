using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace api.Models;

[Table("User")]
public class UserModel
{
    public int Id { get; set; }
    public string? AuthId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}