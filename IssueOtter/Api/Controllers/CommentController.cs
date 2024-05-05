using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Interfaces;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/comment")]
[Authorize]
public class CommentController : ControllerBase
{
  private readonly ICommentService _commentService;

  public CommentController(ICommentService commentService)
  {
    _commentService = commentService;
  }

  [HttpGet("issue/{key}")]
  public async Task<IActionResult> GetAllByIssueKey([FromRoute] string key)
  {
    var comments = await _commentService.GetCommentsByIssueKeyAsync(key);

    return Ok(comments);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCommentRequest createCommentRequest)
  {
    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userAuthId is null)
    {
      return NotFound("User not found");
    }

    var createdComment = await _commentService.CreateCommentAsync(createCommentRequest, userAuthId);

    if (createdComment is null)
    {
      return StatusCode(500, "Server error when creating comment.");
    }

    return Created();
  }
}
