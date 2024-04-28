using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
  public static CommentResponse MapCommentModelToCommentResponse(this Comment commentModel)
  {
    return new CommentResponse
    {
      Id = commentModel.Id,
      Content = commentModel.Content,
      CreatedOn = commentModel.CreatedOn.ToString("F"),
      CreatedById = commentModel.CreatedById,
      CreatedBy = commentModel.CreatedBy?.MapUserToUserResponse(),
      IssueId = commentModel.IssueId
    };
  }

  public static Comment MapCreateCommentRequestToCommentModel(this CreateCommentRequest createCommentRequest)
  {
    return new Comment
    {
      Content = createCommentRequest.Content,
    };
  }
}