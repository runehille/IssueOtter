using System.Security.Claims;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/comment")]
[Authorize]
public class CommentController : ControllerBase
{
  private readonly ICommentRepository _commentRepository;
  private readonly IUserRepository _userRepository;

  public CommentController(ICommentRepository commentRepository, IUserRepository userRepository)
  {
    _commentRepository = commentRepository;
    _userRepository = userRepository;
  }

  [HttpGet("issue/{key}")]
  public async Task<IActionResult> GetAllByIssueKey([FromRoute] string key)
  {
    var comments = await _commentRepository.GetAllByIssueKeyAsync(key);

    var CommentResponse = comments.Select(x => x.MapCommentModelToCommentResponse());

    return Ok(CommentResponse);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCommentRequest createCommentRequest)
  {
    var commentToCreate = createCommentRequest.MapCreateCommentRequestToCommentModel();

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
