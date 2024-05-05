using System.Security.Claims;
using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssueOtter.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
  {
    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userAuthId is null)
    {
      return BadRequest("Access token error. Could not create user.");
    }
    var createdUser = await _userService.CreateUserAync(createUserRequest, userAuthId);

    if (createdUser is null)
    {
      return StatusCode(500, "Something went wrong, could not create user.");
    }

    return Created();
  }
}