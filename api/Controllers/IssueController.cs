using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Issue;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[ApiController]
[Route("api/issue")]
[Authorize]
public class IssueController : ControllerBase
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;

    public IssueController(IIssueRepository issueRepository, IProjectRepository projectRepository)
    {
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var issues = await _issueRepository.GetAllAsync();

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

        var project = await _projectRepository.GetByIdAsync(createIssueRequest.ProjectId);

        if (project is null)
        {
            return BadRequest("Project not found");
        }

        project.IssueCount++;
        issueToCreate.Key = $"{project.Key}-{project.IssueCount}";

        await _projectRepository.UpdateIssueCountAsync(project);
        await _issueRepository.CreateAsync(issueToCreate);

        return Created();
    }
}