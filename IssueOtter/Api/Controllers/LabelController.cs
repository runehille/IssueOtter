using System.Security.Claims;
using IssueOtter.Core.Dtos.Label;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/label")]
[Authorize]
public class LabelController(ILabelService labelService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var labels = await labelService.GetAllLabelsAsync();
        return Ok(labels);
    }

    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> GetByProjectId([FromRoute] int projectId)
    {
        var labels = await labelService.GetLabelsByProjectIdAsync(projectId);
        return Ok(labels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var label = await labelService.GetLabelByIdAsync(id);

        if (label is null) return NotFound("Label not found");

        return Ok(label);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLabelRequest createLabelRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return BadRequest("User not found");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var label = await labelService.CreateLabelAsync(createLabelRequest, userAuthId);

        if (label is null) return StatusCode(500, "Server error when creating label.");

        return CreatedAtAction(nameof(GetById), new { id = label.Id }, label);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLabelRequest updateLabelRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedLabel = await labelService.UpdateLabelAsync(id, updateLabelRequest);

        if (updatedLabel is null) return NotFound($"Label with id {id} not found");

        return Ok(updatedLabel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deletedLabel = await labelService.DeleteLabelAsync(id);

        if (deletedLabel is null) return NotFound($"Label with id {id} not found");

        return NoContent();
    }
}