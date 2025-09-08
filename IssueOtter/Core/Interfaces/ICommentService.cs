using IssueOtter.Core.Dtos.Comment;

namespace IssueOtter.Core.Interfaces;

public interface ICommentService
{
  Task<List<CommentResponse>> GetCommentsByIssueKeyAsync(string key);
  Task<CommentResponse?> CreateCommentAsync(CreateCommentRequest createCommentRequest, string userAuthId);
  Task<CommentResponse?> UpdateCommentAsync(int commentId, UpdateCommentRequest updateCommentRequest, string userAuthId);
  Task<CommentResponse?> DeleteCommentAsync(int commentId, string userAuthId);
}
