using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("User")]
public class UserModel
{
    public int Id { get; set; }
}