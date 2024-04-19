using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
  public static CommentResponse MapCommentModelToCommentResponse(this CommentModel commentModel)
  {
    return new CommentResponse
    {
      Id = commentModel.Id,
      Content = commentModel.Content,
      CreatedOn = commentModel.CreatedOn.ToString("F"),
      CreatedById = commentModel.CreatedById,
      CreatedBy = commentModel.CreatedBy?.MapUserModelToUserResponse(),
      IssueId = commentModel.IssueId
    };
  }

  public static CommentModel MapCreateCommentRequestToCommentModel(this CreateCommentRequest createCommentRequest)
  {
    return new CommentModel
    {
      Content = createCommentRequest.Content,
    };
  }
}