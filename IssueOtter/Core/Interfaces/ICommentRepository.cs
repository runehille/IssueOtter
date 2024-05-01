using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Interfaces;

public interface ICommentRepository
{
  Task<List<Comment>> GetAllByIssueKeyAsync(string key);
  Task<Comment?> CreateAsync(Comment comment);
  Task<Comment?> DeleteAsync(int id);
}
