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

    public IssueController(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
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
        return Created();
    }
}