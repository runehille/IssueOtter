using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Entities;

namespace IssueOtter.Infrastructure.Mappers;

public static class CommentMapper
{
  public static CommentResponse MapCommentToCommentResponse(this Comment comment)
  {
    return new CommentResponse
    {
      Id = comment.Id,
      Content = comment.Content,
      CreatedOn = comment.CreatedOn.ToString("F"),
      CreatedById = comment.CreatedById,
      CreatedBy = comment.CreatedBy?.MapUserToUserResponse(),
      IssueId = comment.IssueId
    };
  }

  public static Comment MapCreateCommentRequestToComment(this CreateCommentRequest createCommentRequest)
  {
    return new Comment
    {
      Content = createCommentRequest.Content,
    };
  }
}