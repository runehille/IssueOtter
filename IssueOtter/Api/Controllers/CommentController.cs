using System.Security.Claims;
using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/comment")]
[Authorize]
public class CommentController : ControllerBase
{
  private readonly ICommentRepository _commentRepository;
  private readonly IUserRepository _userRepository;
  private readonly IIssueRepository _issueRepository;

  public CommentController(ICommentRepository commentRepository, IUserRepository userRepository, IIssueRepository issueRepository)
  {
    _commentRepository = commentRepository;
    _userRepository = userRepository;
    _issueRepository = issueRepository;
  }

  [HttpGet("issue/{key}")]
  public async Task<IActionResult> GetAllByIssueKey([FromRoute] string key)
  {
    var comments = await _commentRepository.GetAllByIssueKeyAsync(key);

    var CommentResponse = comments.Select(x => x.MapCommentToCommentResponse());

    return Ok(CommentResponse);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCommentRequest createCommentRequest)
  {

    var issue = await _issueRepository.GetByKeyAsync(createCommentRequest.IssueKey);

    if (issue is null)
    {
      return NotFound("Issue not found.");
    }

    var commentToCreate = createCommentRequest.MapCreateCommentRequestToComment();

    commentToCreate.IssueId = issue.Id;


    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userAuthId is null)
    {
      return NotFound("User not found");
    }
    var user = await _userRepository.GetByAuthId(userAuthId);

    if (user is null)
    {
      return NotFound("User not found");
    }
    commentToCreate.CreatedById = user.Id;

    try
    {
      await _commentRepository.CreateAsync(commentToCreate);
    }
    catch (DbUpdateException e)
    {
      return BadRequest($"Could not create comment: {e}");
    }

    return Created();
  }
}
