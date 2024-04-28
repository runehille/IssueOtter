using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
  Task<List<CommentModel>> GetAllByIssueKeyAsync(string key);
  Task<CommentModel?> CreateAsync(CommentModel comment);
  Task<CommentModel?> DeleteAsync(int id);
}
