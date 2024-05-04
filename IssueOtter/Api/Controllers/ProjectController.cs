using System.Security.Claims;
using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/project")]
[Authorize]
public class ProjectController : ControllerBase
{
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;
  private readonly IProjectService _projectService;

  public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository, IProjectService projectService)
  {
    _projectRepository = projectRepository;
    _userRepository = userRepository;
    _projectService = projectService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _projectService.GetAllProjectsAsync();

    return Ok(result);
  }

  [HttpGet("{key:alpha}", Name = "GetByKey")]
  public async Task<IActionResult> GetByKey([FromRoute] string key)
  {
    var project = await _projectService.GetProjectByKeyAsync(key);

    if (project is null)
    {
      return BadRequest("Project not found");
    }

    return Ok(project);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateProjectRequest createProjectRequest)
  {

    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userAuthId is null)
    {
      return BadRequest("User not found");
    }

    var project = await _projectService.CreateProjectAsync(createProjectRequest, userAuthId);

    if (project is null)
    {
      return StatusCode(500, "Server error when creating project.");
    }

    return CreatedAtAction(nameof(GetByKey), new { key = project.Key }, project);
  }

  [HttpDelete("{key}")]
  public async Task<IActionResult> DeleteByKey([FromRoute] string key)
  {
    var project = await _projectService.DeleteProjectByKeyAsync(key);

    if (project is null)
    {
      return NotFound("Project not found. Could not delete");
    }

    return NoContent();
  }
}