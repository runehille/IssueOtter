using System.Security.Claims;
using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return BadRequest("Access token error. Could not create user.");
        var createdUser = await userService.CreateUserAsync(createUserRequest, userAuthId);

        if (createdUser is null) return StatusCode(500, "Something went wrong, could not create user.");

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
    {
        var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userAuthId is null) return BadRequest("Access token error. Could not update user.");
        
        var updatedUser = await userService.UpdateUserAsync(updateUserRequest, userAuthId);

        if (updatedUser is null) return NotFound("User not found or could not be updated.");

        return Ok(updatedUser);
    }
}