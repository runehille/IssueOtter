using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Issue;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/issue")]
[Authorize]
public class IssueController : ControllerBase
{
  private readonly IIssueRepository _issueRepository;
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;

  public IssueController(IIssueRepository issueRepository, IProjectRepository projectRepository, IUserRepository userRepository)
  {
    _issueRepository = issueRepository;
    _projectRepository = projectRepository;
    _userRepository = userRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var issues = await _issueRepository.GetAllAsync();

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse());

    return Ok(issuesResponse);
  }

  [HttpGet("{projectId:int}")]
  public async Task<IActionResult> GetAllByProjectId([FromRoute] int projectId)
  {
    var issues = await _issueRepository.GetAllByProjectIdAsync(projectId);

    var issuesResponse = issues.Select(x => x.MapIssueModelToIssueResponse());

    return Ok(issuesResponse);
  }


  [HttpGet("{projectKey:alpha}")]
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
}