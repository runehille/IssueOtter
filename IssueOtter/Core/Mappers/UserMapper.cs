using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Entities;

namespace IssueOtter.Core.Mappers;

public static class UserMapper
{
  public static User MapCreateUserRequestToUser(this CreateUserRequest createUserRequest)
  {
    return new User
    {
      Email = createUserRequest.Email,
      FirstName = createUserRequest.FirstName,
      LastName = createUserRequest.LastName
    };
  }

  public static UserResponse MapUserToUserResponse(this User User)
  {
    return new UserResponse
    {
      Id = User.Id,
      Email = User.Email,
      FirstName = User.FirstName,
      LastName = User.LastName
    };
  }

  public static void MapUpdateUserRequestToUser(this UpdateUserRequest updateUserRequest, User user)
  {
    user.Email = updateUserRequest.Email;
    user.FirstName = updateUserRequest.FirstName;
    user.LastName = updateUserRequest.LastName;
  }
}