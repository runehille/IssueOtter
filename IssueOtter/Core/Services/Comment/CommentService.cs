using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.Comment;

public class CommentService : ICommentService
{
  private readonly ICommentRepository _commentRepository;
  private readonly IIssueRepository _issueRepository;
  private readonly IUserRepository _userRepository;

  public CommentService(ICommentRepository commentRepository, IIssueRepository issueRepository, IUserRepository userRepository)
  {
    _commentRepository = commentRepository;
    _issueRepository = issueRepository;
    _userRepository = userRepository;
  }

  public async Task<CommentResponse?> CreateCommentAsync(CreateCommentRequest createCommentRequest, string userAuthId)
  {
    var issue = await _issueRepository.GetByKeyAsync(createCommentRequest.IssueKey);
    var user = await _userRepository.GetByAuthId(userAuthId);
    var commentToCreate = createCommentRequest.MapCreateCommentRequestToComment();

    if (issue is null || user is null)
    {
      return null;
    }

    commentToCreate.IssueId = issue.Id;
    commentToCreate.CreatedById = user.Id;

    await _commentRepository.CreateAsync(commentToCreate);

    return commentToCreate.MapCommentToCommentResponse();
  }

  public async Task<List<CommentResponse>> GetCommentsByIssueKeyAsync(string key)
  {
    var comments = await _commentRepository.GetAllByIssueKeyAsync(key);
    var commentResponseList = comments.Select(x => x.MapCommentToCommentResponse()).ToList();

    return commentResponseList;
  }
}
