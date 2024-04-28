using System.Security.Claims;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
  private readonly IUserRepository _userRepository;

  public UserController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
  {

    var userAuthId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (await _userRepository.Exists(userAuthId!))
    {
      return NoContent();
    }

    var userToCreate = createUserRequest.MapCreateUserRequestToUser();
    userToCreate.AuthId = userAuthId;

    try
    {
      await _userRepository.CreateAsync(userToCreate);
    }
    catch (DbUpdateException)
    {

      return Conflict("User already in database");
    }

    return Created();
  }
}