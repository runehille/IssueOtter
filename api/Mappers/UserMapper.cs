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
}