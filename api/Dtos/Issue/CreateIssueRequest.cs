using api.Models;

namespace api.Dtos.Issue
{
    public class CreateIssueRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}