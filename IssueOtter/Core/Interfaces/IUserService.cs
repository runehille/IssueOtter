using IssueOtter.Core.Dtos.User;

namespace IssueOtter.Core.Interfaces;

public interface IUserService
{
    Task<UserResponse?> CreateUserAsync(CreateUserRequest createUserRequest, string userAuthId);
    Task<UserResponse?> UpdateUserAsync(UpdateUserRequest updateUserRequest, string userAuthId);
}