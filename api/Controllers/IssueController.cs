using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;

namespace api.Controllers;

[ApiController]
[Route("api/issues")]
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
}