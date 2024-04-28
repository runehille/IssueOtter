using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Project")]
public class ProjectModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AdminId { get; set; }
    public UserModel? Admin { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int CreatedById { get; set; }
    public UserModel? CreatedBy { get; set; }
    public List<IssueModel> Issues { get; set; } = [];
    public int IssueCount { get; set; } = 0;
    public bool IsDeleted { get; set; }
}