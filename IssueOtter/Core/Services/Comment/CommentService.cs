using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.Comment;

public class CommentService(
    ICommentRepository commentRepository,
    IIssueRepository issueRepository,
    IUserRepository userRepository)
    : ICommentService
{
    public async Task<CommentResponse?> CreateCommentAsync(CreateCommentRequest createCommentRequest, string userAuthId)
    {
        var issue = await issueRepository.GetByKeyAsync(createCommentRequest.IssueKey);
        var user = await userRepository.GetByAuthId(userAuthId);
        var commentToCreate = createCommentRequest.MapCreateCommentRequestToComment();

        if (issue is null || user is null) return null;

        commentToCreate.IssueId = issue.Id;
        commentToCreate.CreatedById = user.Id;

        await commentRepository.CreateAsync(commentToCreate);

        return commentToCreate.MapCommentToCommentResponse();
    }

    public async Task<List<CommentResponse>> GetCommentsByIssueKeyAsync(string key)
    {
        var comments = await commentRepository.GetAllByIssueKeyAsync(key);
        var commentResponseList = comments.Select(x => x.MapCommentToCommentResponse()).ToList();

        return commentResponseList;
    }

    public async Task<CommentResponse?> UpdateCommentAsync(int commentId, UpdateCommentRequest updateCommentRequest, string userAuthId)
    {
        var user = await userRepository.GetByAuthId(userAuthId);
        if (user is null) return null;

        var commentToUpdate = updateCommentRequest.MapUpdateCommentRequestToComment();
        var updatedComment = await commentRepository.UpdateAsync(commentId, commentToUpdate);

        if (updatedComment is null) return null;

        return updatedComment.MapCommentToCommentResponse();
    }

    public async Task<CommentResponse?> DeleteCommentAsync(int commentId, string userAuthId)
    {
        var user = await userRepository.GetByAuthId(userAuthId);
        if (user is null) return null;

        var deletedComment = await commentRepository.DeleteAsync(commentId);

        if (deletedComment is null) return null;

        return deletedComment.MapCommentToCommentResponse();
    }
}