using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Dtos.Issue;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/issue")]
[Authorize]
public class IssueController : ControllerBase
{
  private readonly IIssueService _issueService;

  public IssueController(IIssueService issueService)
  {
    _issueService = issueService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var issues = await _issueService.GetAllIssuesAsync();

    return Ok(issues);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById([FromRoute] int id)
  {
    var issue = await _issueService.GetIssueByIdAsync(id);

    if (issue is null)
    {
      return BadRequest("Issue not found");
    }

    return Ok(issue);
  }

  [HttpGet("{key}")]
  public async Task<IActionResult> GetByKey([FromRoute] string key)
  {
    var issue = await _issueService.GetIssueByKeyAsync(key);

    if (issue is null)
    {
      return BadRequest("Issue not found");
    }

    return Ok(issue);
  }


  [HttpGet("project/{projectId:int}")]
  public async Task<IActionResult> GetAllByProjectId([FromRoute] int projectId)
  {
    var issues = await _issueService.GetIssuesByProjectIdAsync(projectId);

    return Ok(issues);
  }


  [HttpGet("project/{projectKey}")]
  public async Task<IActionResult> GetAllByProjectKey([FromRoute] string projectKey)
  {
    var issues = await _issueService.GetIssuesByProjectKeyAsync(projectKey);

    return Ok(issues);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateIssueRequest createIssueRequest)
  {
    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userAuthId is null)
    {
      return BadRequest("Access token user error. The logged in user is not found in database.");
    }

    var issue = await _issueService.CreateIssueAsync(createIssueRequest, userAuthId);

    if (issue is null)
    {
      return StatusCode(500, "Server error when creating issue.");
    }

    return CreatedAtAction(nameof(GetByKey), new { key = issue.Key }, issue);
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var deletedIssue = await _issueService.DeleteIssueByIdAsync(id);

    if (deletedIssue is null)
    {
      return NotFound($"Issue with id {id} not found");
    }

    return NoContent();
  }

  [HttpDelete("{key}")]
  public async Task<IActionResult> DeleteByKey([FromRoute] string key)
  {
    var deletedIssue = await _issueService.DeleteIssueByKeyAsync(key);

    if (deletedIssue is null)
    {
      return NotFound($"Issue with key {key} not found");
    }

    return NoContent();
  }
}