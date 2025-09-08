using System.Security.Claims;
using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/issue")]
[Authorize]
public class IssueController(IIssueService issueService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var issues = await issueService.GetAllIssuesAsync();

        return Ok(issues);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var issue = await issueService.GetIssueByIdAsync(id);

        if (issue is null) return BadRequest("Issue not found");

        return Ok(issue);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetByKey([FromRoute] string key)
    {
        var issue = await issueService.GetIssueByKeyAsync(key);

        if (issue is null) return BadRequest("Issue not found");

        return Ok(issue);
    }


    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> GetAllByProjectId([FromRoute] int projectId)
    {
        var issues = await issueService.GetIssuesByProjectIdAsync(projectId);

        return Ok(issues);
    }


    [HttpGet("project/{projectKey}")]
    public async Task<IActionResult> GetAllByProjectKey([FromRoute] string projectKey)
    {
        var issues = await issueService.GetIssuesByProjectKeyAsync(projectKey);

        return Ok(issues);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIssueRequest createIssueRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null)
            return BadRequest("Access token user error. The logged in user is not found in database.");

        var issue = await issueService.CreateIssueAsync(createIssueRequest, userAuthId);

        if (issue is null) return StatusCode(500, "Server error when creating issue.");

        return CreatedAtAction(nameof(GetByKey), new { key = issue.Key }, issue);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateIssueRequest updateIssueRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedIssue = await issueService.UpdateIssueAsync(id, updateIssueRequest);

        if (updatedIssue is null) return NotFound($"Issue with id {id} not found");

        return Ok(updatedIssue);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] UpdateIssueStatusRequest updateStatusRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null)
            return BadRequest("Access token user error. The logged in user is not found in database.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedIssue = await issueService.UpdateIssueStatusAsync(id, updateStatusRequest, userAuthId);

            if (updatedIssue is null) return NotFound($"Issue with id {id} not found");

            return Ok(updatedIssue);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{key}/status")]
    public async Task<IActionResult> UpdateStatusByKey([FromRoute] string key, [FromBody] UpdateIssueStatusRequest updateStatusRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null)
            return BadRequest("Access token user error. The logged in user is not found in database.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedIssue = await issueService.UpdateIssueStatusByKeyAsync(key, updateStatusRequest, userAuthId);

            if (updatedIssue is null) return NotFound($"Issue with key {key} not found");

            return Ok(updatedIssue);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deletedIssue = await issueService.DeleteIssueByIdAsync(id);

        if (deletedIssue is null) return NotFound($"Issue with id {id} not found");

        return NoContent();
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteByKey([FromRoute] string key)
    {
        var deletedIssue = await issueService.DeleteIssueByKeyAsync(key);

        if (deletedIssue is null) return NotFound($"Issue with key {key} not found");

        return NoContent();
    }
}