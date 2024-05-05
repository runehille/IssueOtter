using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.User;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserResponse?> CreateUserAync(CreateUserRequest createUserRequest, string userAuthId)
  {
    var userToCreate = createUserRequest.MapCreateUserRequestToUser();
    userToCreate.AuthId = userAuthId;

    await _userRepository.CreateAsync(userToCreate);

    var createdUser = await _userRepository.GetByAuthId(userAuthId);

    if (createdUser is null)
    {
      return null;
    }
    return createdUser.MapUserToUserResponse();
  }
}
