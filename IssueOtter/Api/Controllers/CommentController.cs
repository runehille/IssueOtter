using System.Security.Claims;
using IssueOtter.Core.Dtos.Comment;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/comment")]
[Authorize]
public class CommentController(ICommentService commentService) : ControllerBase
{
    [HttpGet("issue/{key}")]
    public async Task<IActionResult> GetAllByIssueKey([FromRoute] string key)
    {
        var comments = await commentService.GetCommentsByIssueKeyAsync(key);

        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest createCommentRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return NotFound("User not found");

        var createdComment = await commentService.CreateCommentAsync(createCommentRequest, userAuthId);

        if (createdComment is null) return StatusCode(500, "Server error when creating comment.");

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return NotFound("User not found");

        var updatedComment = await commentService.UpdateCommentAsync(id, updateCommentRequest, userAuthId);

        if (updatedComment is null) return NotFound("Comment not found or could not be updated.");

        return Ok(updatedComment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return NotFound("User not found");

        var deletedComment = await commentService.DeleteCommentAsync(id, userAuthId);

        if (deletedComment is null) return NotFound("Comment not found or could not be deleted.");

        return Ok(deletedComment);
    }
}