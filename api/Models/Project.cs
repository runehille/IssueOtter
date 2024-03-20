namespace api.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public User? Admin { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
}