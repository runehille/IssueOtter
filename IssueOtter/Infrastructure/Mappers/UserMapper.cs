using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Entities;

namespace IssueOtter.Infrastructure.Mappers;

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
      Email = User.Email,
      FirstName = User.FirstName,
      LastName = User.LastName
    };
  }
}