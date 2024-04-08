using api.Dtos.User;
using api.Models;

namespace api.Mappers;

public static class UserMapper
{
  public static UserModel MapCreateUserRequestToUserModel(this CreateUserRequest createUserRequest)
  {
    return new UserModel
    {
      Email = createUserRequest.Email,
      FirstName = createUserRequest.FirstName,
      LastName = createUserRequest.LastName
    };
  }

  public static UserResponse MapUserModelToUserResponse(this UserModel userModel)
  {
    return new UserResponse
    {
      Email = userModel.Email,
      FirstName = userModel.FirstName,
      LastName = userModel.LastName
    };
  }
}