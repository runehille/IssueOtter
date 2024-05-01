using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Dtos.Issue;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/issue")]
[Authorize]
public class IssueController : ControllerBase
{
  private readonly IIssueRepository _issueRepository;
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;
  private readonly ICommentRepository _commentRepository;

  public IssueController(IIssueRepository issueRepository, IProjectRepository projectRepository, IUserRepository userRepository, ICommentRepository commentRepository)
  {
    _issueRepository = issueRepository;
    _projectRepository = projectRepository;
    _userRepository = userRepository;
    _commentRepository = commentRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var issues = await _issueRepository.GetAllAsync();

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse());

    return Ok(issuesResponse);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById([FromRoute] int id)
  {
    var issue = await _issueRepository.GetByIdAsync(id);

    if (issue is null)
    {
      return BadRequest("Issue not found");
    }

    return Ok(issue);
  }

  [HttpGet("{key}")]
  public async Task<IActionResult> GetByKey([FromRoute] string key)
  {
    var issue = await _issueRepository.GetByKeyAsync(key);


    if (issue is null)
    {
      return BadRequest("Issue not found");
    }

    var issueResponse = issue.MapIssueModelToIssueResponse();

    var commentModels = await _commentRepository.GetAllByIssueKeyAsync(key);

    issueResponse.Comments = commentModels.Select(x => x.MapCommentToCommentResponse()).ToList();

    return Ok(issueResponse);
  }


  [HttpGet("project/{projectId:int}")]
  public async Task<IActionResult> GetAllByProjectId([FromRoute] int projectId)
  {
    var issues = await _issueRepository.GetAllByProjectIdAsync(projectId);

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse());

    return Ok(issuesResponse);
  }


  [HttpGet("project/{projectKey}")]
  public async Task<IActionResult> GetAllByProjectKey([FromRoute] string projectKey)
  {
    var issues = await _issueRepository.GetAllByProjectKeyAsync(projectKey);

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse());

    return Ok(issuesResponse);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateIssueRequest createIssueRequest)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var issueToCreate = createIssueRequest.MapCreateIssueRequestToIssueModel();

    var project = await _projectRepository.GetByKeyAsync(createIssueRequest.ProjectKey);

    if (project is null)
    {
      return BadRequest("Project not found");
    }

    issueToCreate.ProjectId = project.Id;
    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    if (userAuthId is null)
    {
      return BadRequest("User not found");
    }
    var user = await _userRepository.GetByAuthId(userAuthId);

    if (user is not null)
    {
      issueToCreate.CreatedById = user.Id;
      issueToCreate.AssigneeId = user.Id;
      issueToCreate.LastUpdatedById = user.Id;
    }

    project.IssueCount++;
    issueToCreate.Key = $"{project.Key}-{project.IssueCount}";

    try
    {
      await _projectRepository.UpdateIssueCountAsync(project);
      await _issueRepository.CreateAsync(issueToCreate);
    }
    catch (DbUpdateException e)
    {
      return BadRequest($"Could not create issue: {e}");
    }

    return Created();
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var issueToDelete = await _issueRepository.DeleteAsync(id);

    if (issueToDelete is null)
    {
      return NotFound($"Issue with id {id} not found");
    }

    return NoContent();
  }

  [HttpDelete("{key}")]
  public async Task<IActionResult> DeleteByKey([FromRoute] string key)
  {
    var issueToDelete = await _issueRepository.DeleteByKeyAsync(key);

    if (issueToDelete is null)
    {
      return NotFound($"Issue with key {key} not found");
    }

    return NoContent();
  }
}