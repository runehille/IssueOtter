namespace api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    public bool IsOnBoard { get; set; }
    public bool IsDeleted { get; set; }
}