using System.Security.Claims;
using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Interfaces;
using IssueOtter.Infrastructure.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/project")]
[Authorize]
public class ProjectController : ControllerBase
{
  private readonly IProjectRepository _projectRepository;
  private readonly IUserRepository _userRepository;

  public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository)
  {
    _projectRepository = projectRepository;
    _userRepository = userRepository;
  }

  [HttpGet]
  public async Task<ActionResult> GetAll()
  {
    var projects = await _projectRepository.GetAllAsync();

    var projectsResponse = projects.Select(x => x.MapProjectToProjectResponse());

    return Ok(projectsResponse);
  }

  [HttpGet("{key:alpha}")]
  public async Task<ActionResult> GetByKey([FromRoute] string key)
  {
    var project = await _projectRepository.GetByKeyAsync(key);

    if (project is null)
    {
      return BadRequest("Project not found");
    }

    return Ok(project);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateProjectRequest createProjectRequest)
  {

    var projectToCreate = createProjectRequest.MapCreateProjectRequestToProject();

    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    if (userAuthId is null)
    {
      return BadRequest("User not found");
    }
    var user = await _userRepository.GetByAuthId(userAuthId);

    if (user is not null)
    {
      projectToCreate.AdminId = user.Id;
      projectToCreate.CreatedById = user.Id;
    }

    try
    {
      await _projectRepository.CreateAsync(projectToCreate);
    }
    catch (DbUpdateException)
    {
      return BadRequest("Cannot create project");
    }

    return Created();
  }

  [HttpDelete("{key}")]
  public async Task<IActionResult> DeleteByKey([FromRoute] string key)
  {
    var project = await _projectRepository.DeleteByKeyAsync(key);

    if (project is null)
    {
      return NotFound("Project not found. Could not delete");
    }

    return NoContent();
  }
}