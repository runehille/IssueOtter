namespace api.Dtos.Issue;

public class IssueResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
}