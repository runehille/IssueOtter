using System.Security.Claims;
using IssueOtter.Core.Dtos.Project;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/project")]
[Authorize]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await projectService.GetAllProjectsAsync();

        return Ok(result);
    }

    [HttpGet("{key}", Name = "GetByKey")]
    public async Task<IActionResult> GetByKey([FromRoute] string key)
    {
        var project = await projectService.GetProjectByKeyAsync(key);

        if (project is null) return BadRequest("Project not found");

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest createProjectRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return BadRequest("User not found");

        var project = await projectService.CreateProjectAsync(createProjectRequest, userAuthId);

        if (project is null) return StatusCode(500, "Server error when creating project.");

        return CreatedAtAction(nameof(GetByKey), new { key = project.Key }, project);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Update([FromRoute] string key, [FromBody] UpdateProjectRequest updateProjectRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProject = await projectService.UpdateProjectAsync(key, updateProjectRequest);

        if (updatedProject is null) return NotFound($"Project with key {key} not found");

        return Ok(updatedProject);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteByKey([FromRoute] string key)
    {
        var project = await projectService.DeleteProjectByKeyAsync(key);

        if (project is null) return NotFound("Project not found. Could not delete");

        return NoContent();
    }
}