using IssueOtter.Core.Dtos.User;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Mappers;

namespace IssueOtter.Core.Services.User;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserResponse?> CreateUserAsync(CreateUserRequest createUserRequest, string userAuthId)
    {
        var userToCreate = createUserRequest.MapCreateUserRequestToUser();
        userToCreate.AuthId = userAuthId;

        await userRepository.CreateAsync(userToCreate);

        var createdUser = await userRepository.GetByAuthId(userAuthId);

        return createdUser?.MapUserToUserResponse();
    }

    public async Task<UserResponse?> UpdateUserAsync(UpdateUserRequest updateUserRequest, string userAuthId)
    {
        var existingUser = await userRepository.GetByAuthId(userAuthId);
        
        if (existingUser == null)
        {
            return null;
        }

        updateUserRequest.MapUpdateUserRequestToUser(existingUser);

        var updatedUser = await userRepository.UpdateAsync(existingUser);

        return updatedUser?.MapUserToUserResponse();
    }
}