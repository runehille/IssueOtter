using api.Dtos.Project;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/project")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var projects = await _projectRepository.GetAllAsync();

        var projectsResponse = projects.Select(x => x.MapProjectModelToProjectResponse());

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
}