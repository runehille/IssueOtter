namespace IssueOtter.Core.Dtos.Label;

public class LabelResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
}